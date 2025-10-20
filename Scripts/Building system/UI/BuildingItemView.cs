using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingItemView : MonoBehaviour
{
    public string GetName => _image.sprite.name;
    public Transform GetTransform => _image.transform;
    
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _text;
    private Builder _buildingController;
    private string _max;

    private void Awake()
    {
        _buildingController = GetComponentInParent<Builder>();
        _buildingController.ItemAdded += UpdateText;
    }
    public void SetImage(Sprite sprite) {
        _image.sprite = sprite;
    }
    public void SetText(string newText) {
            _max = newText;
            _text.text = $"0/{_max}";
    }
    public void UpdateText(ItemData data, string amount) {
        if (_image.sprite == data.Sprite) {
            _text.text = $"{amount}/{_max}";
        }
    }
}
