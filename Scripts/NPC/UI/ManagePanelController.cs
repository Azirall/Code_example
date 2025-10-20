using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ManagePanelController : MonoBehaviour
{
    [SerializeField]  private RectTransform _container;
    [SerializeField] private GameObject _villagerInfoPrefab;
    
    private NpcService _npcService;
    private DiContainer _diContainer;
    private  List<VillagerInfoView>  _villagersView = new List<VillagerInfoView>();
    [Inject]
    public void Construct(NpcService service,DiContainer diContainer)
    {
        _diContainer = diContainer;
        _npcService = service;
    }

    public void CreateVillagerInPanel(int unlockPrice)
    {
        int villagerId  = _villagersView.Count;
        GameObject villagerInfoGameObject = _diContainer.InstantiatePrefab(_villagerInfoPrefab, _container);
        VillagerInfoView villagerInfoView = villagerInfoGameObject.GetComponent<VillagerInfoView>();
        
        villagerInfoView.Init(villagerId,true,unlockPrice);
        _villagersView.Add(villagerInfoView);
    }

    public void UnlockVillager(int villagerId)
    { 
        _villagersView[villagerId].SetUnlocking(false);
    }

}
