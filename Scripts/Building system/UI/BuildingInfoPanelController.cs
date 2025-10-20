using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class BuildingInfoPanelController : MonoBehaviour
{
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private TextMeshProUGUI _buildingDiscription;
    [SerializeField] private RectTransform _panel;
    private BuildData _data;
    private List<BuildingItemView> _itemViews = new();

    public Transform GetItemTransform(Sprite sprite)
    {
        foreach (var view in _itemViews)
        {
            if (view.GetName == sprite.name)
            {
                return view.GetTransform;
            }
        }
        return null;
    }

    private void Start()
    {
        _data = GetComponent<Builder>().GetBuildData;
        FillPanel();
    }
    private void FillPanel() {

        if (_data.Requirements.Count <= 0) {
            Debug.LogWarning("Массив стоительных предметов пуст");
            return;
        }
  
        foreach (var item in _data.Requirements)
        {
            GameObject panel = Instantiate(_itemPrefab, _panel, false);
            
            BuildingItemView view = panel.GetComponent<BuildingItemView>();
            _itemViews.Add(view);
            _buildingDiscription.text = _data.GetDescription;
            
            view.SetImage(item.Item.Sprite);
            view.SetText(item.Count.ToString());
        }
    }
}
