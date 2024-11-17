using System;
using System.Runtime.CompilerServices;
using System.Timers;

[assembly: InternalsVisibleTo("EditMode")]
namespace ResourceConverter.Scripts
{
  public class Converter
  {
    public bool IsWorking { get; private set; }
    public int InputCount { get; }
    public int OutputCount { get; }
    
    internal readonly ResourceZone _inputZone;
    internal readonly ResourceZone _outputZone;
    private readonly float _interval;

    private float _currentTime;
    private bool _resourcesInProgress;

    public Converter(ResourceZone inputZone, ResourceZone outputZone, int inputCount, int outputCount, float interval)
    {
      if (inputCount < 0 || outputCount < 0 || interval < 0)
      {
        throw new ArgumentOutOfRangeException();
      }

      _inputZone = inputZone;
      _outputZone = outputZone;
      _interval = interval;
      InputCount = inputCount;
      OutputCount = outputCount;

      IsWorking = false;
    }

    public void Update(float deltaTime)
    {
      if (deltaTime <= 0)
        throw new ArgumentOutOfRangeException();
      
      if(!IsWorking)
        return;
      
      _currentTime += deltaTime;

      while (_currentTime >= _interval)
      {
        _currentTime -= _interval;
        
        if(_resourcesInProgress)
          ConvertResources();

        if (!TryGrabResourcesFromInputZone())
        {
          break;
        }
      }
      
      _currentTime = 0;
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
        throw new ArgumentOutOfRangeException();
      
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
      if (_outputZone.ResourceCount == _outputZone.Capacity)
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
