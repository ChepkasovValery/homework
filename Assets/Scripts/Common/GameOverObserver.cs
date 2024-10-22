using Game.Health;
using UnityEngine;

namespace ShootEmUp
{
  public class GameOverObserver : MonoBehaviour
  {
    [SerializeField] private HealthComponent _playerHealth;

    private void OnEnable()
    {
      _playerHealth.OnEnded += GameOver;
    }

    private void OnDisable()
    {
      _playerHealth.OnEnded -= GameOver;
    }

    private void GameOver()
    {
      Time.timeScale = 0;
    }
  }
}