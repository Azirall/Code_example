using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class DayNightSystem
{
    private List<SpriteDimmer> _sprites = new();
    private float _dayDuration = 30f;
    private float _tick = 0.1f;
    [Inject]
    public void Construct(GameSettings gameSettings)
    {
        _dayDuration = gameSettings.GetDayDuration;
    }

    public void RegisterNewSprite(SpriteDimmer spriteDimmer)
    {
        _sprites.Add(spriteDimmer);
    }

    public async UniTask StartLightCycle()
    {
        float timer = 0;
        while (timer <= _dayDuration)
        {
            foreach (var dimmer in _sprites)
            {
                if (_sprites.Count == 0)
                {
                    Debug.LogWarning("список спрайтов для затенения пуст");
                    return;
                }
                
                float factor = Mathf.Lerp(1, 0,timer / _dayDuration);
                dimmer.FadeSprite(factor);
            }
            timer += _tick;
           await UniTask.Delay(TimeSpan.FromSeconds(_tick));
        }
    }
}