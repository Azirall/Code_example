using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class GameDirector : MonoBehaviour
{
    private GameCycleOrchestrator _gameCycleOrchestrator;
    private EnemyOrchestrator _enemyOrchestrator;
    private int _dayCounter = 0;
    [Inject]
    public void Constuct(GameCycleOrchestrator gameCycleOrchestrator,EnemyOrchestrator enemyOrchestrator)
    {
        _gameCycleOrchestrator = gameCycleOrchestrator;
        _enemyOrchestrator = enemyOrchestrator;
    }

    private void OnGameStarted()
    {
        Physics2D.queriesHitTriggers = true;
        
        _enemyOrchestrator.CreateWavesAndEnemy();
        _gameCycleOrchestrator.BeginDayCycle().Forget();
    }

    private void Start()
    {
        OnGameStarted();
    }
}
