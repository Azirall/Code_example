using System;
using UnityEngine;
using UnityEngine.UI;
public class HealthView : MonoBehaviour
{
    [SerializeField] private Image healthLine;
    [SerializeField] private GameObject healthLineObject;

    private void Start()
    {
        if (healthLineObject == null)
        {
            Debug.LogError($"healthLineObject in {gameObject.name} is null");
        }
        else
        {
            healthLineObject.SetActive(false);
        }
    }

    public void UpdateView(float amount)
    {
        if (healthLineObject != null)
        { 
            healthLineObject.SetActive(true);
            healthLine.fillAmount = amount;
        }

        if (amount <= 0)
        {
            healthLineObject.SetActive(false);
        }
    }
}