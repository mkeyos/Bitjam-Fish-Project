using UnityEngine;

public class Health : MonoBehaviour
{
    protected float maxHealth = 1;
    protected float currentHealth;
    public bool dead { get; private set; } = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) {
            dead = true;
            OnDeath();
        }
    }

    protected virtual void OnDeath()
    {
        
    }
}
