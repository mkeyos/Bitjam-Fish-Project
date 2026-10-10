using System.Collections;
using UnityEngine;

public class Enemy2Attack : MonoBehaviour
{
    [SerializeField] Enemy2Values values;
    Enemy2Movement movement;

    bool attacking = false;
    float attackCooldownTimer = 0;

    void Start()
    {
        movement = GetComponent<Enemy2Movement>();
    }

    void Update()
    {
        attackCooldownTimer -= Time.deltaTime;
        if (movement.inAttackRange && !attacking && attackCooldownTimer <= 0) {
            attacking = true;
            StartCoroutine(Attack(movement.lookDirection));
        }
    }


    IEnumerator Attack(Vector2 startLookDir)
    {
        // Wait for attack delay
        yield return new WaitForSeconds(values.attackStartDelay);

        // Something something attack shoot

        // Reset attack state and put attacks on cooldown
        attacking = false;
        attackCooldownTimer = values.attackCooldown;
    }
}
