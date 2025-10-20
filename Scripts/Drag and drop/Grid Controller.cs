using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class CraftGridController : MonoBehaviour
{
    private List<CellView> _cellList = new();
    private IStorageSlots _storage;
    [Inject]
    public void Construct(CraftCellService craftCellService) {
        _storage = craftCellService;
    }
    private void Awake()
    {
        AddCellInDict();
        RegisterCell();
    }
    private void  AddCellInDict() {

        _cellList.AddRange(GetComponentsInChildren<CellView>());
    }
    private void RegisterCell() {
        for (int i = 0; i < _cellList.Count; i++)
        {
            _cellList[i].RegisterCell(i, _storage);
        }
    }
}
