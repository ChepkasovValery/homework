using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("EditMode")]
namespace ResourceConverter.Scripts
{
  public class Converter
  {
    public bool IsWorking { get; private set; }
    public int InputCount { get; }
    public int OutputCount { get; }
    public int InputZoneCapacity => _inputZone.Capacity;
    public int InputZoneAmount => _inputZone.ResourceCount;
    public int OutputZoneCapacity => _outputZone.Capacity;
    public int OutputZoneAmount => _outputZone.ResourceCount;
    
    private readonly ResourceZone _inputZone;
    private readonly ResourceZone _outputZone;
    private readonly float _interval;

    private float _currentTime;
    private bool _resourcesInProgress;

    public Converter(int inputZoneCapacity, int inputZoneAmount, int outputZoneCapacity, int outputZoneAmount,
      int inputCount, int outputCount, float interval)
    {
      if (inputCount < 0)
        throw new ArgumentOutOfRangeException("Input count must be greater than or equal to 0");
      
      if(outputCount < 0)
        throw new ArgumentOutOfRangeException("Output count must be greater than or equal to 0");

      if(interval < 0)
        throw new ArgumentOutOfRangeException("Interval must be greater than or equal to 0");

      _inputZone = new ResourceZone(inputZoneCapacity, inputZoneAmount);
      _outputZone = new ResourceZone(outputZoneCapacity, outputZoneAmount);
      _interval = interval;
      InputCount = inputCount;
      OutputCount = outputCount;

      IsWorking = false;
    }

    public void Update(float deltaTime)
    {
      if (deltaTime <= 0)
        throw new ArgumentOutOfRangeException("Delta time must be greater than or equal to 0");
      
      if(!IsWorking)
        return;
      
      if (_resourcesInProgress)
      {
        Tick(deltaTime);
      }
      else
      {
        TryGrabResourcesFromInputZone();
      }
    }

    private void Tick(float deltaTime)
    {
      _currentTime += deltaTime;

      while (_currentTime >= _interval)
      {
        _currentTime -= _interval;

        ConvertResources();

        if (!TryGrabResourcesFromInputZone())
          break;
      }
    }

    public void Start()
    {
      if (IsWorking)
        return;
      
      TryGrabResourcesFromInputZone();
      
      IsWorking = true;

    }

    public void Stop()
    {
      if(!IsWorking)
        return;
      
      TryReturnResourcesToInputZone();
      
      IsWorking = false;
    }

    public void AddResourcesToInputZone(int count, out int overflowCount)
    {
      if (count < 0)
        throw new ArgumentOutOfRangeException("Count must be greater than or equal to 0");
      
      if (_inputZone.ResourceCount + count > _inputZone.Capacity)
      {
        overflowCount = _inputZone.ResourceCount + count - _inputZone.Capacity;
        
        _inputZone.AddResource(count - overflowCount);
      }
      else
      {
        overflowCount = 0;
        
        _inputZone.AddResource(count);
      }
    }
    
    private bool TryGrabResourcesFromInputZone()
    {
      if (OutputZoneAmount + OutputCount > OutputZoneCapacity)
      {
        return false;
      }
      
      if (_inputZone.WithdrawResource(InputCount))
      {
        _resourcesInProgress = true;
        
        return true;
      }
      
      return false;
    }

    private void TryReturnResourcesToInputZone()
    {
      if(!_resourcesInProgress)
        return;
      
      _inputZone.AddResource(InputCount);

      _resourcesInProgress = false;
    }

    private void ConvertResources()
    {
      _outputZone.AddResource(OutputCount);
      
      _resourcesInProgress = false;
    }
  }
}
