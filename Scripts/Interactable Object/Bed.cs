using UnityEngine;
using Zenject;

public class Bed : BaseInteractableObject
{
    private GameCycleOrchestrator _gameCycleOrchestrator;

    [Inject]
    public void Construct(GameCycleOrchestrator gameCycleOrchestrator) {
       _gameCycleOrchestrator = gameCycleOrchestrator;
    }
    public override bool BeginUse()
    {
        Debug.Log("вызов кровати");
        _gameCycleOrchestrator.TryEndDay();
        return true;
    }

    public override void EndUse()
    {
        
    }
}
