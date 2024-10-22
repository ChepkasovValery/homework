namespace Game.Pool
{
  public interface IPool <T>
  {
    T Get();
    void Return(T obj);
    void Prewarm(int prewarmCount);
  }
}