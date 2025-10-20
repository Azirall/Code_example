using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyOrchestrator
{
    public bool EnemyIsAlive => _enemyManager.EnemyIsAlive();
    private WaveService _waveService;
    private EnemyManager _enemyManager;
    [Inject]
    public void Constuct(WaveService waveService,EnemyManager enemyManager)
    {
        _enemyManager = enemyManager;
        _waveService = waveService;
    }

    public void CreateWavesAndEnemy()
    {
        _waveService.CreateWave();
       var waves = _waveService.GetWaves;
       Wave lastWave = _waveService.GetLastWave;
       _enemyManager.CreateEnemyPool(lastWave);
    }

    public void SpawnEnemy(int dayCount)
    {
        List<WaveEnemy> currentWave = _waveService.GetWaveEnemies(dayCount);
        Debug.Log($"вызвана волна {dayCount} дня");
        _enemyManager.SpawnEnemy(currentWave);
    }
}