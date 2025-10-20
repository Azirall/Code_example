using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
public class ArrowController : MonoBehaviour
{
    [SerializeField] private float _speed;
    Rigidbody2D _rb;
    public void Awake()
    {
      _rb = GetComponent<Rigidbody2D>();
    }

    async public UniTask Move(Vector2 target)
    {

        if (_rb == null) return;
        while (MathF.Abs(_rb.position.x - target.x) > 0.05f)
        { 
            if (_rb == null) return;
            Vector2 pos = Vector2.MoveTowards(_rb.position, target, _speed*Time.deltaTime);
            _rb.MovePosition(pos);
            await UniTask.WaitForFixedUpdate();
        }
    }
}
