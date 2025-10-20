using UnityEngine;
using Zenject;

public class DailyStatsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Wallet>().AsSingle().NonLazy();
        Container.Bind<DayStatsTracker>().AsSingle().NonLazy();
        Container.Bind<StatisticsView>().FromComponentInHierarchy().AsSingle();
        Container.Bind<DayMoneyView>().FromComponentInHierarchy().AsSingle();
    }
}