using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Modules.SaveLoadSystem
{
  public interface IGameRepository
  {
    UniTask<(bool, int)> SetState(Dictionary<string, string> state);
    UniTask<(bool, int, IDictionary<string, string>)> GetState(int version);
  }
}