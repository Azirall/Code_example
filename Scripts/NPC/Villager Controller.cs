using System;
using System.Collections;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using Zenject;


public class VillagerController : MonoBehaviour
{
    private BoxCollider2D _walkZone;
    private Rigidbody2D _rb;
    private float _speed = 1f;
    private Vector2 _homePosition;
    private VillagerView _villagerView;
    private NpcSystem _npcSystem;
    private CancellationTokenSource _cts;
    private int _sortOrder;
    private SpriteRenderer _spriteRenderer;
    [Inject]
    public void Construct(NpcSystem npcSystem)
    {
        _npcSystem = npcSystem;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _villagerView = GetComponent<VillagerView>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public void Init(BoxCollider2D collider, int order)
    {
        _walkZone = collider;
        _sortOrder = order;
    }
    

    public void SetSpawnPosition(Vector2 spawnPosition)
    {
        _homePosition = spawnPosition;
    }

    public async UniTask GoHome()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        
        await UniTask.WaitUntil(() => gameObject.activeInHierarchy);
        
        _spriteRenderer.sortingOrder = _sortOrder;
        _cts = new CancellationTokenSource();
        
        _speed = 1.5f;
        
        await MoveToPosition(_homePosition, _cts.Token);
        
        gameObject.SetActive(false);
    }

    public async UniTask Walk()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        
        await UniTask.WaitUntil(() => gameObject.activeInHierarchy);
        
        _cts = new CancellationTokenSource();
        _speed = 1f;
        var token = _cts.Token;

        while (!token.IsCancellationRequested)
        {
            Vector2 randPoint = GetRandomPoint();
            await MoveToPosition(randPoint, token);
            await UniTask.Delay(TimeSpan.FromSeconds(3), cancellationToken: token);
        }
    }

    public async UniTask GoToJob(Vector2 targetPosition)
    {
        if (_cts != null && gameObject.activeInHierarchy)
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }
        
        await UniTask.WaitUntil(() => gameObject.activeInHierarchy);
        
        _cts = new CancellationTokenSource();
        
        _speed = 1.5f;
        
        await MoveToPosition(targetPosition, _cts.Token);
    }

    private Vector2 GetRandomPoint()
    {
        var b = _walkZone.bounds;
        float x = UnityEngine.Random.Range(b.min.x, b.max.x);
        return new Vector2(x, _homePosition.y);
    }

    private async UniTask MoveToPosition(Vector2 targetPoint,CancellationToken token)
    {
        FaceTo(targetPoint);
        _villagerView.SetWalkAnim();
        while (!token.IsCancellationRequested && Mathf.Abs(_rb.position.x - targetPoint.x) >= 0.05f)
        {
            Vector2 next = Vector2.MoveTowards(_rb.position, targetPoint, _speed * Time.fixedDeltaTime);
            next = new Vector2(next.x,_rb.position.y);
            _rb.MovePosition(next);
            await UniTask.WaitForFixedUpdate();
        }
        if (_rb != null && !token.IsCancellationRequested)
        {
            _rb.position = targetPoint;
            _villagerView.SetIdleAnim();
        }
    }

    private void FaceTo(Vector2 target)
    {
        Vector2 dir = (target - _rb.position).normalized;
        if (dir.x != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Sign(dir.x) * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

    private void OnDestroy()
    {
        _cts?.Cancel();
        _cts?.Dispose();
    }
}
