using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public enum EnemyState
{
    Move,
    Attack
}
public abstract class BaseEnemy : MonoBehaviour
{   
    [SerializeField] protected float attackRange = 2;
    [SerializeField] protected int currentHealth = 4;
    [SerializeField] protected float speed = 1;
    [SerializeField] private Animator animator;
    public bool IsAlive { get; private set; } = true;
    
    private Transform _endPos;
    private HealthView  _healthView;
    private int _maxHealth;
    private IDamageable _currentTarget;
    private int _layerMask;
    private EnemyState _currentState; 
    private Rigidbody2D _rb;
    
    public Vector2 GetPosition => transform.position;
    public void TakeDamage(int damage)
    {
        currentHealth  -= damage;
        _healthView.UpdateView((float)currentHealth/_maxHealth);
        if (currentHealth <= 0)
        {
            IsAlive = false;
            gameObject.SetActive(false);
        }
    }
    public void SetEndPos(Transform endPos)
    {
        _endPos = endPos;
    }

    public virtual async UniTask Attack()
    {
        while (_currentTarget != null && _currentTarget.IsAlive())
        {
            if (!IsAlive) break;
            _currentTarget.TakeDamage(1);
            await UniTask.Delay(TimeSpan.FromSeconds(1));
        }
    }
    public async UniTask InitStateMachine()
    {
        TryFindTarget().Forget();
        while (true){
            switch (_currentState)
            {
                case EnemyState.Move:
                {
                    if (_currentTarget == null)
                    {
                        Move();
                        if (animator != null)
                        {
                            animator.SetBool("Move", true);
                        }
                    }
                    else
                    {
                        _currentState = EnemyState.Attack;
                    }
                    break;
                }
                case EnemyState.Attack:
                {
                    if (animator != null)
                    {
                        animator.SetBool("Move", false);
                    }
                    await Attack();
                    _currentTarget = null;
                    _currentState = EnemyState.Move;
                    TryFindTarget().Forget();
                    break;
                }
            }
            await UniTask.WaitForFixedUpdate();
        }
    }

    private void Awake()
    {
        _healthView = GetComponent<HealthView>();
        _rb = GetComponent<Rigidbody2D>();
        _maxHealth = currentHealth;
        _layerMask = 1 << LayerMask.NameToLayer("Damageable");
        _currentState = EnemyState.Move;
    }
    
    private void Move()
    {
        if (_rb == null) return;
        
        if (IsAlive && Mathf.Abs(_rb.transform.position.x - _endPos.transform.position.x) > 0.1f)
        {
            Vector2 pos = Vector2.MoveTowards(_rb.position,_endPos.transform.position, speed*Time.deltaTime);
            _rb.MovePosition(pos);
        }
    }
    private void OnEnable()
    {
        IsAlive = true;
    }

    private async UniTask TryFindTarget()
    {
        while (true)
        {
            if (_currentTarget == null && gameObject.activeInHierarchy)
            {
                Vector2 origin = transform.position;
                Vector2 direction = Vector2.left;
                RaycastHit2D hit = Physics2D.Raycast(origin, direction, attackRange, _layerMask);
                Debug.DrawRay(origin, direction, Color.red);
                if (hit.collider != null)
                {
                    IDamageable damageable = hit.collider.GetComponent<IDamageable>();
                    if (damageable != null && damageable.IsAlive())
                    {
                        _currentTarget = damageable;
                        return;
                    }
                }
            }
            await UniTask.WaitForSeconds(0.5f);
        }
    }
}