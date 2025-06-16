namespace Game.Modules.Cooldown.Scripts
{
  public interface ICooldown
  {
    bool IsReady();
    void Reset();
  }
}