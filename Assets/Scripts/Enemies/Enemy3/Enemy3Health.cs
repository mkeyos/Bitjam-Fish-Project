using UnityEngine;

public class Enemy3Health : Health
{
    [SerializeField] Enemy3Values values;

    void Start()
    {
        maxHealth = values.maxHealth;
        currentHealth = maxHealth;
    }

    protected override void OnDeath()
    {
        // Disable enemy mechanics and collider
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        Debug.Log("Enemy 3 dead");
        // Destroy enemy, We need these for Barrier script to work properly
        Destroy(gameObject);
    }
}
