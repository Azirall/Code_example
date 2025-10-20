using TMPro;
using UnityEngine;
using Zenject;

public class DayMoneyView : MonoBehaviour
{ 
    private Wallet _wallet;
    private TextMeshProUGUI text;

    [Inject]
    public void Construct(Wallet wallet)
    {
        _wallet = wallet;
    }
    

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    private void UpdateText()
    {
        int money = _wallet.GetMoney;
        text.text = $"{money.ToString()}";
    }
    public void SetNullText() {
        text.text = "0";
    }
    private void OnEnable()
    {
        _wallet.moneyChanged  += UpdateText;
    }
    private void OnDisable()
    {
        _wallet.moneyChanged  -= UpdateText;
    }
}
