using System.Collections.Generic;
using UnityEngine;

public class Wave
{
    private List<WaveEnemy> enemies;
    public List<WaveEnemy> GetWaveEnemies => enemies;
    
    public Wave(List<WaveEnemy> data)
    {
        enemies = data;
    }
}