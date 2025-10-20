using System;
using UnityEngine;

public class TransferItem : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    
    public void SetSprite(Sprite sprite)
    {   
        
        spriteRenderer.sprite = sprite;
    }

    public void SetPos(Transform transform)
    {
        gameObject.transform.position = transform.position;
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }
}
