using UnityEngine;
using Zenject;

public class Stall : BaseInteractableObject
{
    [SerializeField] private GameObject _contractPanel;
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
            _contractPanel.SetActive(true);
            return true;
        }
        return false;
    }

    public override void EndUse()
    {
        _contractPanel.SetActive(false);
    }
}
