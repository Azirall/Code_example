using UnityEngine;
using Zenject;

public class TreeView : MonoBehaviour
{
    private SpriteViewController _spriteView;
    private Animator _anim;
    [Inject]
    public void Construct(SpriteViewController dayViewController) {
        _spriteView = dayViewController;
    }
    private void Awake()
    {
        _anim = GetComponent<Animator>();
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        _anim.speed = Random.Range(0.8f,1.2f);
        _spriteView.RegisterNewSprite(sprite);
    }
}
