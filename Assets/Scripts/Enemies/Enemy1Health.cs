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
        GetComponent<Collider>().enabled = false;
        Debug.Log("Enemy 1 dead");
    }
}
