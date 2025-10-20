using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExtractedItemView : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private RectTransform startPosition;
    [SerializeField] private RectTransform endPosition;
    [SerializeField] private GameObject[] obj;
    [SerializeField] private float _animDuration = 0.5f;
    private Vector3 _scale;
    private Queue<GameObject> _viewObjQueue = new();
    public void ShowExtractedItem(Sprite sprite, int count)
    {
        if (_viewObjQueue.Count > 0)
        {
            GameObject obj = _viewObjQueue.Dequeue();
            SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
            
            obj.transform.localScale = Vector3.zero;
            obj.transform.position = new Vector3(obj.transform.position.x, startPosition.position.y, 0);
            itemImage.sprite = sprite;
            obj.SetActive(true);
            obj.transform.DOMoveY(endPosition.position.y, _animDuration).SetEase(Ease.OutBack);
            DOTween.Sequence().Append(obj.transform.DOScale(_scale, _animDuration)).AppendInterval(0.5f)
                .AppendCallback(() => Hide(obj));
        }
    }

    private void Hide(GameObject viewObj)
    {
        viewObj.SetActive(false);
        _viewObjQueue.Enqueue(viewObj);
    }
    private void Awake()
    {
        _scale = transform.localScale;
        foreach (var view in obj)
        {
            _viewObjQueue.Enqueue(view);
        }
    }
    
}
