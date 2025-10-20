using SpriteGlow;
using UnityEngine;

public class InteractibleGlow : MonoBehaviour {

    private SpriteGlowEffect _spriteGlow;
    private void Awake()
    {
        _spriteGlow = GetComponent<SpriteGlowEffect>();
        _spriteGlow.EnableInstancing = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _spriteGlow.EnableInstancing = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _spriteGlow.EnableInstancing = true;
        }
    }
}