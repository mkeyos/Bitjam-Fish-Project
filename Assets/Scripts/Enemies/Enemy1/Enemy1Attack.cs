using System.Collections;
using UnityEngine;

public class Enemy1Attack : MonoBehaviour
{
    [SerializeField] Enemy1Values values;
    Enemy1Movement movement;

    bool attacking = false;
    float attackCooldownTimer = 0;

    void Start()
    {
        movement = GetComponent<Enemy1Movement>();
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

        // Rotate start vector
        float _x = startLookDir.x;
	    float _y = startLookDir.y;

	    float _angle = -values.attackAngle/2 * Mathf.Deg2Rad;
	    float _cos = Mathf.Cos(_angle);
	    float _sin = Mathf.Sin (_angle);

	    float _x2 = _x * _cos - _y * _sin;
	    float _y2 = _x * _sin + _y * _cos;

	    Vector2 startVector = new Vector2(_x2, _y2);

        // Cast multiple raycasts in a cone in front of the enemy
        for (int i = 0; i <= values.attackAngle; i += 10)
        {
            // Rotate the raycast direction by i (10) degrees
            float x = startVector.x;
            float y = startVector.y;

            float angle = i * Mathf.Deg2Rad;
            float cos = Mathf.Cos(angle);
            float sin = Mathf.Sin (angle);

            float x2 = x * cos - y * sin;
            float y2 = x * sin + y * cos;

            Vector2 vector = new Vector2(x2, y2);

            // Raycast itself
            RaycastHit2D hit = Physics2D.Raycast(transform.position, vector, values.attackRange, values.raycastLayers);
            // Make sure there's actually something to hit
            if (hit.collider != null && hit.collider.gameObject != null)
            {
                // Get hit object
                GameObject hitObj = hit.collider.gameObject;

                // Check for player hit and deal damage
                if (hitObj.tag == "Player") {
                    hitObj.GetComponent<Health>().Damage(values.attackDamage);
                    break;
                }
            }
        }

        // Reset attack state and put attacks on cooldown
        attacking = false;
        attackCooldownTimer = values.attackCooldown;
    }
}
