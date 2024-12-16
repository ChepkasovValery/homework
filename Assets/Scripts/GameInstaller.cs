using Game;
using Game.World;
using Modules;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private World _world;
    [SerializeField] private int _levelsCount = 9;
    
    public override void InstallBindings()
    {
        WorldInstaller.Install(Container);
        InputInstaller.Install(Container);
        ScoresInstaller.Install(Container);
        CoinInstaller.Install(Container, _coinPrefab, _world);
        DifficultyInstaller.Install(Container, _levelsCount);
        SnakeInstaller.Install(Container);
        UIInstaller.Install(Container);
        GameStateInstaller.Install(Container);
    }
}