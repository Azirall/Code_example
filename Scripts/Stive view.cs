using UnityEngine;
using Zenject;

public class StiveView : MonoBehaviour
{
    private SpriteViewController _spriteView;
    [Inject]
    public void Construct(SpriteViewController spriteView) {
        _spriteView = spriteView;
    }
    private void Awake()
    {
        SpriteRenderer render = GetComponent<SpriteRenderer>();
        _spriteView.RegisterNewSprite(render);
    }
}
