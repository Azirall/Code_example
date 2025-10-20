using System;
using System.Diagnostics;
using _Project.Scripts.Mining_System;
using UnityEngine;

public class StaminaSystem
{   
    
    public Action<float> staminaChanged;
    public bool StaminaIsNull => ChekStamina();
    private float _baseStamina;

    private float _currentStamina;
    
    private MiningSystem _extractionOrchestrator;
    public StaminaSystem(GameSettings gameSettings) {
        
        _baseStamina = gameSettings.BaseStamina;
        _currentStamina = _baseStamina;
        
    }
    public void ResetStamina()
    {
        _currentStamina = _baseStamina;
        staminaChanged?.Invoke(_currentStamina / _baseStamina);
    }
    public bool TrySubtract(float price) {
        if (!StaminaCanBeUsed(price)) return false;
        _currentStamina -= price;
        staminaChanged?.Invoke(_currentStamina/ _baseStamina);
        return true;

    }
    private bool StaminaCanBeUsed(float price) {
        if (_currentStamina >= price) return true;
        else return false;
    }

    private bool ChekStamina()
    {
        if (_currentStamina <= 0.5f)
        {
            return true;
        }
        return false;
    }
    
}
