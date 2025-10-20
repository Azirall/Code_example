using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;


public class CraftResultCell : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private GameObject _imageObject;
    [SerializeField] private TextMeshProUGUI _textCount;
    private ICraftResultSlots _currentService;
    private DragDropOrchestrator _orchestrator;
    private CraftService _craftService;
    private CanvasGroup _canvasGroup;
    private Image _image;
    private int _index;
    [Inject]
    public void Constuct(DragDropOrchestrator dragDropOrchestrator, CraftService craftService)
    {
        _craftService = craftService;
        _orchestrator = dragDropOrchestrator;

    }
    private void Awake()
    {
        _image = _imageObject.GetComponent<Image>();
        _canvasGroup = _imageObject.GetComponent<CanvasGroup>();
        _craftService.RecipeFound += UpdateView;
        _currentService = _craftService;

    }


    public void OnPointerClick(PointerEventData eventData)
    {
        var click = eventData.button == PointerEventData.InputButton.Right ? ClickType.Right : ClickType.Left;
        _orchestrator.OnClick(_currentService, _index, click);
    }

    private void UpdateView(ItemStack stack)
    {
        if (stack != null)
        {
            Sprite sprite = stack.Item.Sprite;
            int count = stack.Count;

            _image.sprite = sprite;
            _canvasGroup.alpha = 1f;
            _textCount.text = count == 1 ? "" : count.ToString();
        }
        else
        {
            _image.sprite = null;
            _canvasGroup.alpha = 0;
            _textCount.text = "";
        }
    }
    private void OnDestroy()
    {
        _craftService.RecipeFound -= UpdateView;
    }
}

