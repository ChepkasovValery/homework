namespace Game.Pool
{
  public interface ICreator<T>
  {
    T Create();
  }
}