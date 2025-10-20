using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Zenject;

public class WaveService
{
    private List<Wave> _waves = new();
    public List<Wave> GetWaves => _waves;
    public Wave GetLastWave => _waves[^1];
    public List<WaveEnemy> GetWaveEnemies(int waveIndex) =>  _waves[waveIndex].GetWaveEnemies;
    public void CreateWave()
    {
        for (int i = 1; i < 6; i++)
        {
            List<WaveEnemy> waveEnemyList = new();
            
            WaveEnemy waveEnemy = new WaveEnemy(EnemyType.Zombie,2*i);
            
            waveEnemyList.Add(waveEnemy);
            
            _waves.Add(new Wave(waveEnemyList));
        }
    }
}