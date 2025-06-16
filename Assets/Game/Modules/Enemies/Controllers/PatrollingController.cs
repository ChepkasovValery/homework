using System;
using Game.Modules.Patrolling.Scripts;
using Modules.Health.Scripts;
using Zenject;

namespace Game.Modules.Enemies.Controllers
{
  public class PatrollingController : IInitializable, IDisposable
  {
    private readonly IPatrolling _patrolling;
    private readonly IHealth _health;

    public PatrollingController(IHealth health, IPatrolling patrolling)
    {
      _health = health;
      _patrolling = patrolling;
    }

    public void Initialize()
    {
      _health.OnEnded += Stop;
    }

    public void Dispose()
    {
      _health.OnEnded -= Stop;
    }

    private void Stop()
    {
      _patrolling.Stop();
    }
  }
}