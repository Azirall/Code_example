using UnityEngine;
using Zenject;

public enum ClickType { Left, Right }

public class DragDropOrchestrator : MonoBehaviour
{
    [SerializeField] private GameObject _dragItemPrefab;
    [SerializeField] private RectTransform _canvasParent;
    [Inject] CraftCellService craftCellService;
    public ItemData _randItem;

    private ItemStack _currentItem;
    private DragItem _dragItem;
    public void OnClick(object service, int index, ClickType click)
    {
        switch (service)
        {
            case IStorageSlots storage:

                if (_currentItem == null && click == ClickType.Left)
                {
                    if (storage.TryExtract(index, out var extracted, ItemAmountEnum.All))
                    {
                        _currentItem = extracted;
                        CreateDragItem(_currentItem);
                        return;
                    }
                }
                else if (_currentItem != null && click == ClickType.Left)
                {
                    if (storage.CanAccept(index, _currentItem))
                    {

                        storage.TryInsert(index, _currentItem, out var rest, ItemAmountEnum.All);
                        HandleInsertResult(rest);
                    }
                }
                else if (_currentItem == null && click == ClickType.Right)
                {

                    if (storage.TryExtract(index, out var extracted, ItemAmountEnum.Half))
                    {
                        _currentItem = extracted;
                        CreateDragItem(_currentItem);
                        return;
                    }

                }
                else if (_currentItem != null && click == ClickType.Right)
                {
                    if (storage.CanAccept(index, _currentItem))
                    {

                        storage.TryInsert(index, _currentItem, out var rest, ItemAmountEnum.One);
                        HandleInsertResult(rest);
                    }
                }
                break;
            case ICraftResultSlots result:
                if (_currentItem == null && click == ClickType.Left)
                {
                   result.TryTakeAll(out var extracted);
                   if (extracted == null) return;
                  _currentItem = extracted;
                  CreateDragItem(_currentItem);
                  return;
                }
                break;
        }
    }
    private void CreateDragItem(ItemStack stack) {
        int count = stack.Count;
        Sprite sprite = stack.Item.Sprite;

        _dragItem.Init(sprite, count);
    }
    private void HandleInsertResult(ItemStack rest) {
        if (rest != null)
        {
            _currentItem = rest;
            CreateDragItem(rest);
            return;
        }
        else
        {
            _currentItem = null;
            _dragItem.ClearDragItem();
            return;
        }
    }
    private void Update()
    {
        if (_dragItem != null && Application.isPlaying)
        {
            _dragItem.transform.position = Input.mousePosition;
        }
    }
    private void Awake()
    {
        _dragItem = Instantiate(_dragItemPrefab, _canvasParent).GetComponent<DragItem>();
        craftCellService.Initialize();
    }
}
