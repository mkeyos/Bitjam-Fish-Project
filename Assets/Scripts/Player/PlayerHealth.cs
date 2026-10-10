using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] PlayerValues values;

    void Start()
    {
        maxHealth = values.maxHealth;
        currentHealth = maxHealth;
    }

    protected override void OnDeath()
    {
        // Whatvever needs to happen after death
        Debug.Log("Player dead");
    }
}
