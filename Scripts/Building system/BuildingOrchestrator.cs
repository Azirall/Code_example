using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class BuildingsOrchestrator
{
    private BuildingService _buildingService;
    private InventoryService _inventoryService;
    private ResourceTransferController _resourceTransferController;
    [Inject]
    public void Construct(BuildingService buildingService,InventoryService inventoryService,
                          ResourceTransferController resourceTransferController) {
        
        _resourceTransferController = resourceTransferController;
        _inventoryService = inventoryService;
        _buildingService = buildingService;
        
    }
    public async UniTask TryAddResources(Builder builder)
    {

        BuildingData data = _buildingService.GetBuildingData(builder);
        if (data == null)
        {
            return;
        }
        BuildingProgress progress = data.GetProgress();

        foreach (var need in progress.GetRemaining())
        {
            _inventoryService.ExtractForBuilding(need, out var extracted);
            
            BuildingInfoPanelController view = builder.GetInfoPanel; 
            Transform itemTransform = view.GetItemTransform(extracted.Item.Sprite);
            
            await _resourceTransferController.SendItem(extracted.Item.Sprite,itemTransform);
            
            progress.Contribute(extracted, out int accepted);
            builder.UpdateText(extracted.Item, accepted.ToString());

            if (progress.CheckProgress())
            {
                builder.CompleteBuilding();
            }
        }
    }

}
