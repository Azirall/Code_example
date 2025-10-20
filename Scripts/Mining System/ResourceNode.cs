using System;
using System.Collections;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class ResourceNode : BaseInteractableObject
{

    [SerializeField] private ItemData extractedItem;
    [SerializeField] private ItemData requiredTool;
    [SerializeField] private Transform workPos;
    [SerializeField] private NodeType nodeType;
    
    
    public Transform GetWorkPos => workPos;
    public event Action<ItemData> Tick;
    public event Action<ItemData> WorkerTick;

    private float _tickTimeForWorkers = 7f;
    
    private InventoryService _inventoryService;
    private ExtractedItemView _extractedView;
    private CancellationTokenSource _cts;
    private StaminaSystem _staminaSystem;
    private MiningSystem _orchestrator;
    [Inject]
    public void Construct(MiningSystem orchestrator, InventoryService inventoryService, 
                          StaminaSystem staminaSystem) {
        _inventoryService = inventoryService;
        _staminaSystem = staminaSystem;
        _orchestrator = orchestrator; ;
    }

    public override bool BeginUse()
    {
        if (!_staminaSystem.StaminaIsNull)
        {
            _cts = new CancellationTokenSource();
            TickLoop(1.5f,_cts.Token).Forget();
            return true;
        }
        return false;
    }

    async UniTask TickLoop( float tickTime, CancellationToken token) {
        while (!token.IsCancellationRequested) {
            if(_inventoryService.CanAccepItem(extractedItem) && !_staminaSystem.StaminaIsNull)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(tickTime), cancellationToken: token);
                Tick?.Invoke(extractedItem);
                _extractedView.ShowExtractedItem(extractedItem.Sprite, 1);
            }
            if(_staminaSystem.StaminaIsNull)
            {
                _player.EndInteraction();
                EndUse();
            }
        }
    }
    public void Start()
    {
        _orchestrator.RegisterNewNode(this, nodeType);
        _extractedView = GetComponent<ExtractedItemView>();
    }

    public override void EndUse()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = null;
    }

    public void StartWorkProcess()
    {
        Debug.Log("работник прибыл в рощу");
        StartCoroutine(nameof(SetPassiveExtractionCoroutine));
    }

    IEnumerator SetPassiveExtractionCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_tickTimeForWorkers);
            WorkerTick?.Invoke(extractedItem);
            _extractedView.ShowExtractedItem(extractedItem.Sprite,1);
        }
    }

    public void StopWorkProcess()
    {
        StopAllCoroutines();
    }
    
}
