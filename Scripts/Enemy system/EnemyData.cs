using UnityEngine;
[CreateAssetMenu(menuName = "Enemy", fileName = "newEnemy")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private GameObject obj;
    [SerializeField] private EnemyType type;
    public GameObject GetEnemyObject => obj;
    public EnemyType GetEnemyType => type;
}