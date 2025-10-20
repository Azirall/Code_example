using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpriteViewController : MonoBehaviour
{
    
    private int _dayDuration;
    private float _timer = 0;
    private static readonly int BlendID = Shader.PropertyToID("_BlendGlobalUnique");
    private List<SpriteRenderer> _backgroundSprites = new();

    [Inject]
    public void Construct(GameSettings settings) {
        _dayDuration = settings.GetDayDuration;
    }
    public void RegisterNewSprite(SpriteRenderer sprite) {
      _backgroundSprites.Add(sprite);
    }
    IEnumerator StartDay() {
        while (_timer < _dayDuration) {
            
            _timer += Time.deltaTime;
            float t = Mathf.Clamp01(_timer / _dayDuration);

            Shader.SetGlobalFloat(BlendID, t);
            ReduseSpriteGamma(t);
            
            yield return null;
        }
        Shader.SetGlobalFloat(BlendID, 1);
    }
    private void ReduseSpriteGamma(float t) {

        float value = Mathf.Lerp(1f, 0.6f, t);
        if (_backgroundSprites.Count == 0) return;
        foreach (var spriteRenderer in _backgroundSprites)
        {
            Color.RGBToHSV(spriteRenderer.color, out var h, out var s, out var v);
            spriteRenderer.color = Color.HSVToRGB(h, s, value, true);
        }
    }
    private void Awake()
    {
        Shader.SetGlobalFloat(BlendID, 0);
        
    }
    public void ResetDayAnimation() {
        _timer = 0;
        Shader.SetGlobalFloat(BlendID, 0);
    }

    public void StartDayAnimation() {
        StartCoroutine(nameof(StartDay));
    }
}
