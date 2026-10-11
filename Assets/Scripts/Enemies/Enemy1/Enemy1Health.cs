using UnityEngine;

public class Enemy1Health : Health
{
    [SerializeField] Enemy1Values values;

    void Start()
    {
        maxHealth = values.maxHealth;
        currentHealth = maxHealth;
    }

    protected override void OnDeath()
    {
        // Disable enemy mechanics and collider
        GetComponent<Enemy1Movement>().enabled = false;
        GetComponent<Enemy1Attack>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        Debug.Log("Enemy 1 dead");
        // Destroy enemy, We need these for Barrier script to work properly
        Destroy(gameObject);
    }
}
