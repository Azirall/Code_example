using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using Zenject;

public class ContractView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private TextMeshProUGUI _itemCount;
    [SerializeField] private TextMeshProUGUI _contractRevard;
    [SerializeField] private GameObject _completeLine;
    [SerializeField] private Image _progressImage;
    [SerializeField] private float _holdDuration = 1.5f;

    private ContractSystem _contractSystem;
    private  float _elapsed;
    private int _contractId;
    private bool _isActive = true;
    [Inject]
    public void Construct(ContractSystem contractSystem)
    {
        _contractSystem = contractSystem;
    }
    
    public void Init(Sprite sprite, string name, string rewardText, int index, int count)
    {
        _isActive = true;
        _itemCount.text = count == 1 ? "" : count.ToString();
        _itemImage.sprite = sprite;
        _itemName.text = name;
        _contractRevard.text = rewardText;
        _contractId = index;
        _completeLine.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        StopAllCoroutines();
        if (_isActive)
        {
            StartCoroutine(nameof(HoldRoutine));
        }
    }

    public void OnPointerUp(PointerEventData eventData) => Cancel();
    public void OnPointerExit(PointerEventData eventData) => Cancel();

    IEnumerator HoldRoutine()
    {
        _elapsed = 0f;
        while (_elapsed < _holdDuration)
        {
            _elapsed += Time.unscaledDeltaTime;
            if (_progressImage != null)
                _progressImage.fillAmount = _elapsed / _holdDuration;
            yield return null;
        }

        if (_progressImage != null) _progressImage.fillAmount = 1f;
        SubmitContract();
        _progressImage.fillAmount = 0f;
    }

   private  void Cancel()
    {
        StopAllCoroutines();
        _progressImage.fillAmount = 0f;
    }
   private void SubmitContract()
    {
        if (_contractSystem.TryCompleteContract(_contractId))
        {
            _isActive = false;
            _completeLine.SetActive(true);
        }

    }
}
