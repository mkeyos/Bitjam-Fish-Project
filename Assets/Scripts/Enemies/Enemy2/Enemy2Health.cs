using UnityEngine;

public class Enemy2Health : Health
{
    [SerializeField] Enemy2Values values;

    void Start()
    {
        maxHealth = values.maxHealth;
        currentHealth = maxHealth;
    }

    protected override void OnDeath()
    {
        // Disable enemy mechanics and collider
        GetComponent<Enemy2Movement>().enabled = false;
        GetComponent<Enemy2Attack>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        Debug.Log("Enemy 2 dead");
        // Destroy enemy, We need these for Barrier script to work properly
        Destroy(gameObject);
    }
}
