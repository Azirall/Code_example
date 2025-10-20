using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(int amount);

    public void RestoreHealth();

    public Transform GetTransform();
    
    public bool IsAlive();
}