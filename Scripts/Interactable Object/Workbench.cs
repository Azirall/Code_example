using System;
using UnityEngine;
using Zenject;

public class Workbench : BaseInteractableObject
{
    [SerializeField] private GameObject _craftPanel;
    [SerializeField] private StaminaSystem _staminaSystem;

    [Inject]
    public void Construct(StaminaSystem staminaSystem)
    {
        _staminaSystem = staminaSystem;
    }

    public override bool BeginUse()
    {
        if (!_staminaSystem.StaminaIsNull)
        { 
            _craftPanel.SetActive(true);
            return true;
        }
        return false;
    }

    public override void EndUse()
    {
        _craftPanel.SetActive(false);
    }
}
