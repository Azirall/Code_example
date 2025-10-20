using UnityEngine;
[System.Serializable]
public struct WaveEnemy
{
    [SerializeField] private  EnemyType _type;
    [SerializeField] private  int _count;
    public int GetCount => _count;
    public EnemyType GetEnemyType => _type; 
    public WaveEnemy(EnemyType type, int count)
    {
        _type = type;
        _count = count;
    }
}