using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
public class Builder: MonoBehaviour, IInteractable
{
    [SerializeField] private bool alreadyBuilt = false;
    [SerializeField] private BuildingView buildingView;
    [SerializeField] private BaseBuilding building;
    [SerializeField] private BuildingInfoPanelController infoPanel;
    
    public BuildingInfoPanelController GetInfoPanel => infoPanel;
    public event Action<ItemData, string> ItemAdded;
    public bool AlreadyBuilt => alreadyBuilt;
    public BuildData GetBuildData => _buildData;
    
    private BuildingsOrchestrator _buildingOrchestrator;
    private BuildingService _buildingService;
    private PlayerOrchestrator _player;
    private BuildData _buildData;
    
    [Inject]
    public void Construct(BuildingsOrchestrator buildingOrchestrator, BuildingService buildingService) {

        _buildingOrchestrator = buildingOrchestrator;
        _buildingService = buildingService;
    }
    public void CompleteBuilding() {
        _buildingService.ChangeBuildingStatus(this,BuildingStatus.Complete);
        buildingView.CompleteBuild();
        alreadyBuilt = true;
        
        IDamageable damageable = GetComponentInParent<IDamageable>();
        if (damageable != null)
        {
            damageable.RestoreHealth();
        }
    }
    public void DestroyBuilding()
    {
        buildingView.SetDestroySprite();
        _buildingService.ChangeBuildingStatus(this,BuildingStatus.NotBuild);
        
    }
    public void UpdateText(ItemData stack, string amount) {
        ItemAdded?.Invoke(stack,amount);
    }
    private void Awake()
    {
        _buildData = building.GetBuildData;
        _buildingService.AddNewBuilding(this, building);
        
        CheckBuildingStatus();
    }

    private void CheckBuildingStatus()
    {
        if (alreadyBuilt)
        {
            CompleteBuilding();
        }

        if (!alreadyBuilt)
        {
            building.DestroyBuilding();
        }
    }

    public bool BeginUse()
    {
        if (buildingView.ReadyAcceptItem)
        {
            _buildingOrchestrator.TryAddResources(this).Forget();
        }
        return true;
    }

    public void EndUse()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _player = other.GetComponent<PlayerOrchestrator>();
            _player.SetInteractableObject(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _player.SetInteractableObject(null);
        }
    }
}
