using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Zenject;

public class EnemyManager : SerializedMonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform enemyEndPos;
    [OdinSerialize, ShowInInspector] private Dictionary<EnemyType, GameObject> _enemiesPrefab = new();
    
    private Dictionary<EnemyType,List<GameObject>> _enemiesPool = new();
    private Vector2Int _sortOrder;

    [Inject]
    public void Construct(GameSettings gameSettings)
    {
        _sortOrder = gameSettings.GetEnemySortOrder;
    }

    public void CreateEnemyPool(Wave lastWave)
    {
        var currentWave = lastWave.GetWaveEnemies;
        foreach (WaveEnemy enemy in currentWave)
        {
          int enemyCount = enemy.GetCount;
          EnemyType type = enemy.GetEnemyType;
          CreateEnemy(enemyCount,type);
        }
    }
    private void CreateEnemy(int count, EnemyType type)
    {
        _enemiesPool[type] = new List<GameObject>();
        
        GameObject enemy = _enemiesPrefab[type];
        int sortOrder = _sortOrder.x;
        
        for (int i = 0; i < count; i++)
        {
          GameObject obj = Instantiate(enemy, container.transform);
          obj.SetActive(false);
          
          BaseEnemy baseEnemy = obj.GetComponent<BaseEnemy>();
          baseEnemy.SetEndPos(enemyEndPos);
          
          SpriteRenderer sprite = obj.GetComponent<SpriteRenderer>();
          sprite.sortingOrder = sortOrder;
          sortOrder++;
          
          _enemiesPool[type].Add(obj);
        }
    }

    public void SpawnEnemy(List<WaveEnemy> enemies)
    {
        foreach (var enemy in enemies)
        {
            EnemyType type = enemy.GetEnemyType;
            int enemyCount = enemy.GetCount;
            for (int j = 0; j < enemyCount; j++)
            {
                GameObject obj = _enemiesPool[type][j];
                BaseEnemy baseEnemy = obj.GetComponent<BaseEnemy>();
                baseEnemy.SetEndPos(enemyEndPos);
                float randPoint = Random.Range(-3f, 3f);
                obj.transform.position = spawnPoint.position + new Vector3(randPoint,0,0);
                obj.SetActive(true);
                baseEnemy.InitStateMachine().Forget();
            }

        }
    }

    public bool EnemyIsAlive()
    {
        foreach (var enemyList in _enemiesPool.Values)
        {
            foreach (GameObject enemy in enemyList)
            {
                if (enemy.activeInHierarchy)
                {
                    return true;
                }
            }
            return false;
        }
        return false;
    }
    
}