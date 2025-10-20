using System.Collections.Generic;
using _Project.Scripts.NPC;
using UnityEngine;

public class NpcService
{
    private List<VillagerData> _villagersData = new();
    public List<VillagerData> VillagersData => _villagersData;
    public void AddNewVillager(VillagerData villagerData) {
        _villagersData.Add(villagerData);
    }
    public void RemoveVillager(VillagerData villagerData) {
        _villagersData.Remove(villagerData);
    }
    public VillagerData GetVillagerData(int index) {
        return _villagersData[index];
    }
    public int GetVillagersCount()
    {
        return _villagersData.Count;
    }
    public void ChangeVillagerJob(int villagerId, JobEnum job)
    {
        _villagersData[villagerId].ChangeJob(job);
        Debug.Log($"жителю {villagerId} назначена работа {job}");
    }

}
