using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace Modules.SaveLoadSystem
{
  public class NetworkGameRepository : IGameRepository, IInitializable
  {
    private readonly string _uri;
      
    private const string SAVE_VERSION_KEY = "save_version";
    private int _currentVersion;

    public NetworkGameRepository(string uri)
    {
      _uri = uri;
    }

    public void Initialize()
    {
      _currentVersion = GetLastVersion();
    }

    public async UniTask<(bool, int)> SetState(Dictionary<string, string> state)
    {
      string jsonState = JsonConvert.SerializeObject(state);
        
      UnityWebRequest request = UnityWebRequest.Put($"{_uri}/save?version={_currentVersion + 1}", jsonState);
      await request.SendWebRequest();

      bool result = request.result == UnityWebRequest.Result.Success;

      if (result)
      {
        IncrementVersion();
      }

      return (result, _currentVersion);
    }

    public async UniTask<(bool, int, IDictionary<string, string>)> GetState(int version)
    {
      UnityWebRequest request = UnityWebRequest.Get($"{_uri}/load?version={version}");
      await request.SendWebRequest();
      
      bool result = request.result == UnityWebRequest.Result.Success;

      if (result)
      {
        return (result, version, JsonConvert.DeserializeObject<Dictionary<string, string>>(request.downloadHandler.text) ?? new Dictionary<string, string>());
      }
      
      return (false, 0, null);
    }
    
    private int GetLastVersion()
    {
      return PlayerPrefs.GetInt(SAVE_VERSION_KEY);
    }
    
    private void IncrementVersion()
    {
      _currentVersion++;

      PlayerPrefs.SetInt(SAVE_VERSION_KEY, _currentVersion);
    }
  }
}

