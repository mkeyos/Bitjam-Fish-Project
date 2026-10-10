using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected float maxHealth = 1;
    protected float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) OnDeath();
    }

    protected virtual void OnDeath()
    {
        
    }
}
