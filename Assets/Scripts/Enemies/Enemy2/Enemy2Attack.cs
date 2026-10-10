using System.Collections;
using UnityEngine;

public class Enemy2Attack : MonoBehaviour
{
    [SerializeField] Enemy2Values values;
    [SerializeField] GameObject bulletPrefab;
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
        Debug.Log("Start shooting");
        // Wait for attack delay
        yield return new WaitForSeconds(values.attackStartDelay);

        // Something something attack shoot
        if (values.bulletsPerAttack > 1)
        for (int i = 1; i <= values.bulletsPerAttack; i++)
        {
            // Wait between bullets
            if (i > 1) yield return new WaitForSeconds(values.firerate / (values.bulletsPerAttack - 1));

            // Randomize bullet spread for each bullet
            float x = startLookDir.x;
            float y = startLookDir.y;

            float angle = Random.Range(-values.attackSpread/2, values.attackSpread/2) * Mathf.Deg2Rad;
            float cos = Mathf.Cos(angle);
            float sin = Mathf.Sin (angle);

            float x2 = x * cos - y * sin;
            float y2 = x * sin + y * cos;

            Vector2 bulletDirection = new Vector2(x2, y2);

            // Create the bullet
            Vector2 position = transform.position;
            GameObject bullet = Instantiate(bulletPrefab, position + bulletDirection * 1.2f, Quaternion.Euler(bulletDirection));

            // Flip bullet sprite when shooting it towards left
            SpriteRenderer bulletSprite = bullet.GetComponentInChildren<SpriteRenderer>();
            //bulletSprite.flipY = Vector2.Angle(bulletDirection, Vector2.right) > 90;

            // Add force to the bullet to make it fly
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(bulletDirection * values.bulletSpeed, ForceMode2D.Impulse);

            // Set max range for the bullet
            bullet.GetComponent<Bullet>().maxRange = values.attackRange;
        }
        Debug.Log("End shooting");
        // Reset attack state and put attacks on cooldown
        attacking = false;
        attackCooldownTimer = values.attackCooldown;
    }
}
