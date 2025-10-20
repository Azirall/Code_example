using UnityEngine;

public class BuildingData
{
    public BuildingType GetBuildingType => _buildData.GetBuildingType;
    public BuildingStatus GetStatus => _status;
    public BaseBuilding GetBuildingClass => _buildingController;
    public Transform GetTransform => _buildingController.transform;
    public Builder GetBuilder => _builder;
    private Builder _builder;
    private BuildData _buildData;
    private BaseBuilding _buildingController;
    private BuildingProgress _progress;
    private BuildingStatus _status;
    public BuildingData(Builder builder, BaseBuilding buildingController) {

        _buildData = builder.GetBuildData;
        _builder =  builder;
        _progress = new(_buildData);
        _buildingController = buildingController;
        _status = BuildingStatus.NotBuild;
    }
    public BuildingProgress GetProgress() {
        return _progress;
    }
    

    public void UpdateStatus(BuildingStatus status)
    {
        _status = status;
    }
}
