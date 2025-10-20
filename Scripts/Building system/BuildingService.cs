using System.Collections.Generic;
using UnityEngine;

public class BuildingService
{
    private List<BuildingData> _buildingsList = new();

    public void AddNewBuilding(Builder builder, BaseBuilding building)
    {
        BuildingData newBuilding = new(builder,building);
        _buildingsList.Add(newBuilding);
    }
    public BuildingData GetBuildingData(Builder builder) {
        foreach (var data in _buildingsList)
        {
            if (data.GetBuilder == builder)
            {
                return data;
            }
        }
        return null;
    }

    public void ChangeBuildingStatus(Builder builder, BuildingStatus status)
    {
        foreach (var data in _buildingsList)
        {
            if (data.GetBuilder == builder)
            {
                data.UpdateStatus(status);
            }
        }
    }
    public List<Transform> GetAllSpawnPositions() {

        List<Transform> housesSpawnPos = new();
        
        foreach (var build in _buildingsList)
        {
            if (build.GetBuildingClass is VillagerHome home)
            {
                if (build.GetStatus == BuildingStatus.Complete)
                {
                    housesSpawnPos.Add(home.GetSpawnPos);
                }
            }
        }
        return housesSpawnPos;
    }
}