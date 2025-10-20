using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StaminaView : MonoBehaviour
{
    [SerializeField] private Image staminaLine;
    private StaminaSystem _staminaSystem;
    private float width;
    [Inject] public void Construct(StaminaSystem stamina) {
        _staminaSystem = stamina;
        
    }
    private void Awake()
    {
        _staminaSystem.staminaChanged += UpdateLine;
        width = staminaLine.rectTransform.sizeDelta.x;
    }
    private void UpdateLine(float amount) {

        RectTransform rectTransform = staminaLine.rectTransform;

        float newWidth = width * amount;

        rectTransform.sizeDelta = new Vector2(newWidth, rectTransform.sizeDelta.y);
    }
}
