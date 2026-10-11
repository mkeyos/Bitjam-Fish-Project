using UnityEngine;

public class Enemy2Health : Health
{
    [SerializeField] Enemy2Values values;
    private Animator animator;

    void Start()
    {
        maxHealth = values.maxHealth;
        currentHealth = maxHealth;
        animator = GetComponent<Animator>(); // 
    }

    protected override void OnDeath()
    {
        animator.SetTrigger("Dead");
        // Disable enemy mechanics and collider
        GetComponent<Enemy2Movement>().enabled = false;
        GetComponent<Animator>().SetBool("Dead", true);
        GetComponent<Enemy2Attack>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        Debug.Log("Enemy 2 dead");
        // Destroy enemy, We need these for Barrier script to work properly
        Destroy(gameObject, 3f);
    }
}
