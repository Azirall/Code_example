using System.Collections.Generic;
using _Project.Scripts.NPC;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class VillagerInfoView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _villagerImage;
    [SerializeField] private TMP_Dropdown _dropdown;
    [SerializeField] private GameObject _lockedView;
    [SerializeField] private TextMeshProUGUI _unlockPrice;
    private NpcSystem _npcSystem;
    private int _villagerId;

    [Inject]
    public void Construct(NpcSystem npcSystem)
    {
        _npcSystem = npcSystem;
    }

    public void Init(int villagerId, bool lockStatus, int unlockPrice)
    {
        _unlockPrice.text = unlockPrice.ToString();
        _villagerId = villagerId;
        Debug.Log($"инициализация, статус {lockStatus}");
        _lockedView.SetActive(lockStatus);
    }

    public void SetUnlocking(bool isUnlocking)
    {  
        Debug.Log($"разблокировка, статус {isUnlocking}");
       _lockedView.SetActive(isUnlocking);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _npcSystem.TryUnlockVillager(_villagerId);
    }

    private void Start()
    {
        _dropdown.onValueChanged.AddListener(OnDropdownChanged);
        _dropdown.ClearOptions();
        var names = System.Enum.GetNames(typeof(JobEnum));
        _dropdown.AddOptions(new List<string>(names));
    }
    private void OnDropdownChanged(int index)
    {
        _npcSystem.SetVillagerJob(_villagerId,index);
    }
}
