
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;


public class GameInstaller : MonoInstaller
{
    [SerializeField] private GameSettings gameSettings;
    public override void InstallBindings()
    {
        Container.BindInstance(gameSettings).AsSingle();
        
        Container.Bind<GameDirector>().FromComponentInHierarchy().AsSingle();
        Container.Bind<Wallet>().AsSingle().NonLazy();
        
        Container.Bind<PlayerInteraction>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ManageBoard>().FromComponentInHierarchy().AsSingle();
        Container.Bind<Workbench>().FromComponentInHierarchy().AsSingle();
        
        
        Container.BindInterfacesAndSelfTo<MiningSystem>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<NpcSystem>().AsSingle().NonLazy();
        Container.Bind<ContractSystem>().AsSingle().NonLazy();
        Container.Bind<DayNightSystem>().AsSingle().NonLazy();
        Container.Bind<StaminaSystem>().AsSingle().NonLazy();
        
        Container.Bind<ResourceTransferController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ContractPanelController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ManagePanelController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ChestGirldController>().FromComponentInHierarchy().AsSingle();
        
        Container.Bind<NpcManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<EnemyManager>().FromComponentInHierarchy().AsSingle();
        
        Container.Bind<DragDropOrchestrator>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GameCycleOrchestrator>().AsSingle().NonLazy();
        Container.Bind<BuildingsOrchestrator>().AsSingle().NonLazy();
        Container.Bind<EnemyOrchestrator>().AsSingle().NonLazy();

        Container.BindInterfacesAndSelfTo<CraftCellService>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<CraftService>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<ChestService>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<WaveService>().AsSingle().NonLazy();

        Container.Bind<InventoryService>().AsSingle().NonLazy();
        Container.Bind<BuildingService>().AsSingle().NonLazy();
        Container.Bind<NpcService>().AsSingle().NonLazy();
    }
}