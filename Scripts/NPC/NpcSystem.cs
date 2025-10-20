using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.NPC;
using UnityEngine;
using Zenject;

public class NpcSystem : IInitializable
{
    private Wallet _wallet;
    private NpcService _npsService;
    private NpcManager _npcManager;
    private BuildingService _buildingService;
    private ManagePanelController _workPanelController;
    private MiningSystem _miningSystem;
    private int _numberVillagerPerHouse;
    private int _unlockPrice = 1;
    private List<JobEnum> _villagerJobs = new();
    [Inject]
    public void Construct(NpcService service, NpcManager manager,
                          GameSettings settings, BuildingService buildingService,
                          ManagePanelController workPanelController, Wallet wallet,
                          MiningSystem miningSystem)
    {
        _miningSystem = miningSystem;
        _wallet = wallet;
        _npcManager = manager;
        _npsService = service;
        _buildingService = buildingService;
        _workPanelController = workPanelController;
        _numberVillagerPerHouse = settings.NumberVillagerPerHouse;
    }

    public async void SpawnVillagers()
    {
        int npcCount = _npsService.GetVillagersCount();
        var housesList = _buildingService.GetAllSpawnPositions();
        int housesCount = housesList.Count;
        
        Debug.Log($"построенных домов {housesCount}");
        if (housesCount == 0) return;
        if (npcCount < housesCount * _numberVillagerPerHouse)
        {
            CreateVillagerData(npcCount, housesList);
            Debug.Log($"создаем {npcCount} жителей");
        }

        await _npcManager.SpawnVillagers();
    }

    public void SendVillagersHome()
    {
        _npcManager.SendAllVillagersAtHouses();
    }

    public void TryUnlockVillager(int npcId)
    {
        Debug.Log($"попытка купить");
        Debug.Log($"имеется денег{_wallet.GetMoney}, требуется {_unlockPrice}  : {_wallet.GetMoney >= _unlockPrice}");
        if (_wallet.GetMoney >= _unlockPrice)
        {
            _workPanelController.UnlockVillager(npcId);
            _wallet.SubtractMoney(_unlockPrice);
        }
    }

    public void SetVillagerJob(int villagerId, int job)
    {
        JobEnum villagerJob = _villagerJobs[job];
        _npsService.ChangeVillagerJob(villagerId,villagerJob);
        _npcManager.ChangeVillagerJob(villagerId, villagerJob);
    }
    public void Initialize()
    {
        CreateJobList();
    }
    private void CreateVillagerData(int npcCount, List<Transform> housesPositions)
    {
        int needNpс = housesPositions.Count * _numberVillagerPerHouse;
        int npcForSpawn = needNpс-npcCount;
        
        for (int i = 0; i < npcForSpawn; i++)
        {
            VillagerData newVillager = new(JobEnum.None,NpcStatus.Walk,null);
            _npsService.AddNewVillager(newVillager);
            _npcManager.CreateVillagers();
            _workPanelController.CreateVillagerInPanel(_unlockPrice);
        }
    }
    private void CreateJobList()
    {
        _villagerJobs.Clear();
         _villagerJobs = Enum.GetValues(typeof(JobEnum)).Cast<JobEnum>().ToList();
    }
}
