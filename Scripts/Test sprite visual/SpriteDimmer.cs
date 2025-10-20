using System;
using UnityEngine;
using Zenject;

public class SpriteDimmer : MonoBehaviour
{
    [SerializeField] private float minFade = 0.4f;
    private DayNightSystem _dayNightSystem;
    private SpriteRenderer _spriteRenderer;
    private float _maxFade;
    private Color _col;
    private float _h, _s, _v;
    [Inject]
    public void Construct(DayNightSystem dayNightSystem)
    {
        _dayNightSystem = dayNightSystem;
    }

    private void Awake()
    {
        _dayNightSystem.RegisterNewSprite(this);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        _col = _spriteRenderer.color;
        Color.RGBToHSV(_col, out  _h, out  _s, out _v);
        _maxFade = _v;
    }

    public void FadeSprite(float amount)
    {
        float factor = Mathf.Lerp(minFade,_maxFade, amount); 
        _spriteRenderer.color = Color.HSVToRGB(_h, _s, factor);  
    }
}