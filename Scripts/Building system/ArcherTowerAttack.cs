using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ArcherTowerAttack: MonoBehaviour
{
    [SerializeField] private int arrowCount = 10;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawnPoint;
    [SerializeField] private Transform arrowContainer;
    [SerializeField] private Builder builder;
    private Queue<BaseEnemy>  _targetQueue = new();
    private Stack<GameObject> _arrowStack = new();
    private int _maxArchers = 1;
    private int _activeArchers = 0;
    private bool _canShoot = false;
    private void Start()
    {
        CreateArrow();
    }

    public void CanShoot(bool value)
    {
        _canShoot = value;
    }

    private void OnTriggerEnter2D(Collider2D enemy)
    {
        if (enemy.CompareTag("EnemyTarget"))
        {
            BaseEnemy controller = enemy.GetComponent<BaseEnemy>();
            _targetQueue.Enqueue(controller);
        }
    }
    async private UniTask BeginShooting()
    {
        if (_activeArchers < _maxArchers)
        {
            BaseEnemy target = _targetQueue.Dequeue();
            _activeArchers++;
            while (target.IsAlive)
            {
                if(!_canShoot) return;
                if (_arrowStack.Count > 0)
                {
                    Shoot(target.GetPosition, target);
                    await UniTask.Delay(TimeSpan.FromSeconds(1f));
                }

                await UniTask.Delay(TimeSpan.FromSeconds(1f));
            }
            _activeArchers--;
        }
    }

    private async void Shoot(Vector2 target, BaseEnemy enemy)
    {
        Vector2 direction = target - (Vector2)arrowSpawnPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        GameObject arrow = _arrowStack.Pop();
        ArrowController arrowController = arrow.GetComponent<ArrowController>();
        
        arrow.transform.position = arrowSpawnPoint.position;
        arrow.transform.rotation = Quaternion.FromToRotation(Vector3.right, direction);
        arrow.SetActive(true);
        
        await arrowController.Move(target);
        enemy.TakeDamage(2);
        arrow.SetActive(false);
        _arrowStack.Push(arrow);
    }

    private void CreateArrow()
    {
        for (int i = 0; i < arrowCount; i++)
        {
            GameObject arrow = Instantiate(arrowPrefab,arrowContainer);
            arrow.SetActive(false);
            _arrowStack.Push(arrow);
        }
    }

    private void Update()
    {
        if (_targetQueue.Count > 0 && builder.AlreadyBuilt)
        {
            BeginShooting().Forget();
        }
    }
}
