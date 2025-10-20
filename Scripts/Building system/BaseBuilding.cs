using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
[System.Serializable]
public abstract class BaseBuilding : MonoBehaviour, IDamageable
{
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private HealthView  healthView;
    [SerializeField] private BuildData buildData;
    [SerializeField] protected Builder builder; 
    [SerializeField] private int health;
    
    public BuildData GetBuildData => buildData;
    
    private int _currentHealth;
    protected bool _isAlive = false;
    private void Awake()
    {
       builder =  GetComponentInChildren<Builder>();
    }
    public void TakeDamage(int amount)
    {
        _currentHealth  -= amount;
        healthView.UpdateView((float)_currentHealth/health);
        if (_currentHealth <= 0)
        {
            DestroyBuilding();
        }
    }
    public virtual void RestoreHealth()
    {
        _currentHealth = health;
        boxCollider2D.enabled = true;
        _isAlive = true;
    }
    public Transform GetTransform() => gameObject.transform;
    public bool IsAlive() => _isAlive;

    public virtual void DestroyBuilding()
    {
        _isAlive = false;
        builder.DestroyBuilding();
        boxCollider2D.enabled = false;
    }
}
