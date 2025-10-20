using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;
public class CellView : MonoBehaviour, IPointerClickHandler
{ 
    [SerializeField] private GameObject imageObject;
    [SerializeField] private TextMeshProUGUI textCount;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image image;
    private IStorageSlots _currentService;
    private DragDropOrchestrator _orchestrator;
    private int _index;
    [Inject]
    public void Constuct(DragDropOrchestrator dragDropOrchestrator) {

        _orchestrator = dragDropOrchestrator;
    }

    public  void ApplyNewItem(Sprite sprite, int count)
    {
        image.sprite = sprite;
        canvasGroup.alpha = 1f;
        textCount.text = count == 1 ? "" : count.ToString(); 
    }

    public  void CleanCell()
    {   
        canvasGroup.alpha = 0f;
        image.sprite = null;
        textCount.text = "";
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        var click = eventData.button == PointerEventData.InputButton.Right ? ClickType.Right : ClickType.Left;
        _orchestrator.OnClick(_currentService,_index,click);
    }

    public virtual void RegisterCell(int index,IStorageSlots service) {
        _index = index;
        _currentService = service;
        
        _currentService.Changed += UpdateView;
    }

    private void UpdateView(int index, ItemStack stack) {
        if (_index == index) {
            if (stack == null)
            {
                CleanCell();
            }
            else {
                ApplyNewItem(stack.Item.Sprite, stack.Count);
            }
        }
    }
    private void OnDestroy()
    {
        _currentService.Changed -= UpdateView;
    }
}

