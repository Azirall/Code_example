using System;
using System.Collections.Generic;
using _Project.Scripts.NPC;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class NpcManager : MonoBehaviour 
{
    [SerializeField] private GameObject npcPrefab;
    [SerializeField] private BoxCollider2D walkZone;
    private BuildingService _buildingService;
    private List<VillagerController>  _villagers = new List<VillagerController>();
    private List<Transform> _housesSpawnPos = new List<Transform>();
    private MiningSystem _miningSystem;
    private NpcService _npcService;
    private DiContainer _diContainer;
    private Vector2Int _sortOrder;
    [Inject]
    public void Construct(BuildingService buildingService,MiningSystem miningSystem,
                          NpcService npcService, DiContainer diContainer, GameSettings gameSettings)
    {
        _npcService = npcService;
        _diContainer = diContainer;
        _miningSystem = miningSystem;
        _buildingService = buildingService;
        _sortOrder = gameSettings.VillagerSortOrder;
    }

    private const int VillagersPerHouse = 2;
    private int _neededVillagers = 0;

    public void CreateVillagers()
    {
        if (npcPrefab == null)
        {
            Debug.LogWarning("В нпс менеджер не назначен префаб нпс");
            return;
        }

        _housesSpawnPos = _buildingService.GetAllSpawnPositions();
        _neededVillagers = _housesSpawnPos.Count * VillagersPerHouse;
        int order = _sortOrder.x;
        for (int i = _villagers.Count; i < _neededVillagers; i++)
        {
            GameObject villager = _diContainer.InstantiatePrefab(npcPrefab);
            VillagerController villagerController = villager.GetComponent<VillagerController>();
            SpriteRenderer spriteRender = villager.GetComponent<SpriteRenderer>();
            
            spriteRender.sortingOrder = order;
            villagerController.Init(walkZone,order);
            order++;
            
            villager.SetActive(false);
            _villagers.Add(villagerController);
        }
        
        for (int i = 0; i < _neededVillagers; i++)
        {
            int houseIndex = i / VillagersPerHouse;
            var pos = _housesSpawnPos[houseIndex].position;

            GameObject villager = _villagers[i].gameObject;
            villager.transform.position = pos;
            villager.GetComponent<VillagerController>().SetSpawnPosition(pos);
            villager.SetActive(false);
        }
    }
    
    public void SendAllVillagersAtHouses()
    {
        foreach (var villagerController in _villagers)
        {
            villagerController.GoHome().Forget();
        }
    }

    public void ChangeVillagerJob(int villagerId, JobEnum job)
    {
        VillagerController villagerController = _villagers[villagerId];
        SetJob(villagerController, job);
    }

    public async UniTask SpawnVillagers()
    {
        for (int i = 0; i < _neededVillagers; i++)
        {
            GameObject villager = _villagers[i].gameObject;
            VillagerController villagerController = villager.GetComponent<VillagerController>();
            JobEnum job = _npcService.GetVillagerData(i).GetJob;
            
            villager.gameObject.SetActive(true);
            SetJob(villagerController,job);
            
            await UniTask.Delay(TimeSpan.FromSeconds(2));
        }
    }
    private async void SetJob(VillagerController villager, JobEnum job)
    {
        if (job == JobEnum.None)
        {
            villager.Walk().Forget();
        }

        if (job == JobEnum.Miner)
        {
            Transform pos = _miningSystem.GetNodeTransform(NodeType.Mine);
            await villager.GoToJob(new Vector2(pos.position.x, pos.position.y));
            _miningSystem.AddWorkerToNode(NodeType.Mine);
        }

        if (job == JobEnum.Woodman)
        {
            Transform pos = _miningSystem.GetNodeTransform(NodeType.Grove);
            await  villager.GoToJob(new Vector2(pos.position.x, pos.position.y));
            _miningSystem.AddWorkerToNode(NodeType.Grove);
        }
    }
}



