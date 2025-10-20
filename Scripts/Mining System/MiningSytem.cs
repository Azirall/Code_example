using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MiningSystem: IInitializable
{

    private float _extractionPrice;
    private Dictionary<NodeType,ResourceNode> _nodes = new Dictionary<NodeType, ResourceNode>();
    private InventoryService _inventory;
    private StaminaSystem _stamina;
    private ChestService _chestService;
    [Inject]
    public void Construct(InventoryService inventoryService, GameSettings settings,
                          StaminaSystem stamina,ChestService chestService) {
        _chestService = chestService;
        _inventory = inventoryService;
        _extractionPrice = settings.GetExtractionPrice;
        _stamina = stamina;
    }

    public void Initialize()
    {
        _inventory.Initialize();
    }

    public void RegisterNewNode(ResourceNode node, NodeType type) {
            
        _nodes.Add(type, node);
        node.Tick += ExtractItem;
        node.WorkerTick += WorkerExtractItem;
    }
    private void ExtractItem(ItemData item) {

        if (_stamina.TrySubtract(_extractionPrice))
        {
            _inventory.AddNewItem(item);
        }
    }

    private void WorkerExtractItem(ItemData item)
    {
        _chestService.AddNewItem(item);
    }
    public Transform GetNodeTransform(NodeType type)
    {
        return _nodes[type].GetWorkPos;
    }

    public void AddWorkerToNode(NodeType type)
    {
        ResourceNode resourceNode = _nodes[type];
        resourceNode.StartWorkProcess();
    }

    public void RemoveWorkersFromNode()
    {
        foreach (ResourceNode resourceNode in _nodes.Values)
        {
            resourceNode.StopWorkProcess();
        }
    }

}
