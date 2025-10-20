using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class ResourceTransferController : MonoBehaviour
{
    [SerializeField] private List<TransferItem> _items = new List<TransferItem>();
    [SerializeField] private GameObject _player;
    private Queue<TransferItem> _queue = new Queue<TransferItem>();
    
    private void Awake()
    {
        foreach (var item in _items)
        {
            _queue.Enqueue(item);
        }
    }

    public async UniTask SendItem(Sprite sprite, Transform pos)
    {
        Sequence seq = DOTween.Sequence();
        
        if (sprite == null) return;
        TransferItem item = _queue.Dequeue();
        
        item.SetSprite(sprite);
        item.SetPos(_player.transform);
        item.gameObject.SetActive(true);
        
        seq.Append(item.transform.DOMove(pos.position, 0.2f).SetEase(Ease.Linear)).
            Append(item.transform.DOScale(item.transform.localScale*1.2f, 0.15f).SetEase(Ease.OutQuad).SetLoops(1, LoopType.Yoyo));
        
        await seq.Play().ToUniTask();
       _queue.Enqueue(item);
       item.gameObject.SetActive(false);
    }
}
