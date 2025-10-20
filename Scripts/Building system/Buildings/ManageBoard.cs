using System;
using UnityEngine;
using Zenject;

public class ManageBoard : BaseInteractableObject
{
    public event Action Show;
    public event Action Hide;
    
    private NpcService _npcService;

    [Inject]
    public void Construct(NpcService npcService)
    {
        _npcService = npcService;
    }
    public override bool BeginUse()
    {
        if (_npcService.GetVillagersCount() != 0)
        {
            Show?.Invoke();
            return true;
        }
        return false;
    }

    public override void EndUse()
    {
        Hide?.Invoke();
    }
}

