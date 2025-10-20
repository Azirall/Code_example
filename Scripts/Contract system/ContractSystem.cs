using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class ContractSystem
{
    private List<ItemData> _items = new List<ItemData>();
    private List<Contract>  _contracts = new List<Contract>();
    private int _numberContractPerVillager;
    private Wallet _wallet;
    private NpcService _npcService;
    private InventoryService _inventoryService;
    private ContractPanelController _controller;
    [Inject]
    public void Construct(GameSettings settings,Wallet wallet,
        ContractPanelController controller,InventoryService inventoryService,
        NpcService npcService)
    {
        _wallet = wallet;
        _controller = controller;
        _items = settings.FirstLvlOrders;
        _npcService = npcService;
        _inventoryService = inventoryService;
        _numberContractPerVillager = settings.NumberContractPerVillager;
    }

    public void GenerateContracts()
    {
        _contracts.Clear();
        int numberContracts = _npcService.GetVillagersCount()*_numberContractPerVillager;
        if (numberContracts == 0) return;
        
        for (int i = 0; i < numberContracts; i++)
        {
            
            int randomNumber = Random.Range(0, _items.Count);
            int randomCount = Mathf.Max(1,Random.Range(1, _items[randomNumber].MaxStackCount));
            
            ItemStack newStack = new(_items[randomNumber], randomCount);
            Contract newContract = new Contract(newStack);
            _contracts.Add(newContract);
        }
        _controller.UpdateContractView(_contracts);
    }

    public bool TryCompleteContract(int numberContract)
    {
        if (_inventoryService.TryExtractForContract(_contracts[numberContract].Stack))
        {
            _wallet.AddMoney(1);
            Debug.Log(_wallet.GetMoney);
            _contracts[numberContract] = null;
            return true;
        }
        else
        {
            return false;
        }
        

    }
}
