using System;
using Cysharp.Threading.Tasks;
using Zenject;

public class GameCycleOrchestrator
{
    public event Action<int> DayCounterChange;
    
    private EnemyOrchestrator _enemyOrchestrator;
    private ContractSystem _contractSystem;
    private DayNightSystem _dayNightSystem;
    private StaminaSystem _staminaSystem;
    private MiningSystem _miningSystem;
    private NpcSystem _npcSystem;
    private int _dayCounter = 0;
    private bool _dayMayBeEnded = false;
    
    [Inject]
    public void Constuct(StaminaSystem staminaSystem, NpcSystem npcSystem,
                         ContractSystem contractSystem, MiningSystem miningSystem
                         ,DayNightSystem dayNightSystem, EnemyOrchestrator enemyOrchestrator )
    {
        _enemyOrchestrator = enemyOrchestrator;
        _dayNightSystem = dayNightSystem;
        _contractSystem = contractSystem;
        _staminaSystem = staminaSystem;
        _miningSystem = miningSystem;
        _npcSystem = npcSystem;
    }
    public async UniTask BeginDayCycle() {
        _dayMayBeEnded = false;
        _dayCounter++;
        DayCounterChange?.Invoke(_dayCounter);
        
        _staminaSystem.ResetStamina();
        _npcSystem.SpawnVillagers();
        _contractSystem.GenerateContracts();
        if(_dayCounter >= 4)
        {
            _enemyOrchestrator.SpawnEnemy(3);
        }
        await _dayNightSystem.StartLightCycle();
        BeginNightCycle();
        _dayMayBeEnded = true;
        
    }
    public void BeginNightCycle() {
        _npcSystem.SendVillagersHome();
        _miningSystem.RemoveWorkersFromNode(); 
    }

    public void TryEndDay()
    {
        if (!_enemyOrchestrator.EnemyIsAlive && _dayMayBeEnded)
        {
            BeginDayCycle().Forget();
        }
    }
}
