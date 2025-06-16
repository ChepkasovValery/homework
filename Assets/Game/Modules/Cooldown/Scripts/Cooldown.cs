using Game.Modules.Cooldown.Configs;
using UnityEngine;

namespace Game.Modules.Cooldown.Scripts
{
  public class Cooldown : ICooldown
  {
    private readonly CooldownConfig _config;
    
    private float _lastTime;
    
    public Cooldown(CooldownConfig config)
    {
      _config = config;
    }

    public bool IsReady() => Time.time - _lastTime >= _config.Cooldown;

    public void Reset()
    {
      _lastTime = Time.time;
    }
  }
}