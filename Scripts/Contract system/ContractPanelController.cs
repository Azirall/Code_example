using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ContractPanelController : MonoBehaviour
{
    [SerializeField] private GameObject _contractPrefab;
    [SerializeField] private Transform _contractContainer;
    
    private List<GameObject> _contractsList = new List<GameObject>();
    private ContractSystem _contractSystem;
    private DiContainer _container;

    [Inject]
    public void Construct(ContractSystem contractSystem,DiContainer container)
    {
        _container = container;
        _contractSystem = contractSystem;
        
    }

    public void UpdateContractView(List<Contract> contracts)
    {
        if (contracts.Count > _contractsList.Count)
        {
            int needCreateContracts = contracts.Count - _contractsList.Count;
            CreateContracts(needCreateContracts);
        }
        FillContracts(contracts);
    }

    private void FillContracts(List<Contract> contracts)
    {
        for (int j = 0; j < contracts.Count; j++) 
        {
            Contract contract = contracts[j];
            GameObject go = _contractsList[j];
            
            ContractView contractView = go.GetComponent<ContractView>();
            
            Sprite sprite = contract.Sprite;
            string name = contract.Name;
            int count = contract.Count;
            
            contractView.Init(sprite, name, "1",j, count);
        }
    }

    private void CreateContracts(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject go = _container.InstantiatePrefab(_contractPrefab, _contractContainer);
            _contractsList.Add(go);
        }
    }
}
