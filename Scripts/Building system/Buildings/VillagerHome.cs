using UnityEngine;

public class VillagerHome : BaseBuilding
{
    [SerializeField] private Transform _spawnPos;
    public Transform GetSpawnPos => _spawnPos;

}