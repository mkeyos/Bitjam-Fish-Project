using System.Collections;
using UnityEngine;

public class Enemy3Attack : MonoBehaviour
{
    [SerializeField] Enemy3Values values;
    
    GameObject player;

    bool attacking = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        // Check for player proxomity
        if (Vector2.Distance(player.transform.position, transform.position) <= values.targetingRange && !attacking) {
            attacking = true;
            StartCoroutine(Attack());
        }
    }


    IEnumerator Attack()
    {
        Debug.Log("Exploding");
        // Wait for attack delay
        yield return new WaitForSeconds(values.attackStartDelay);
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, values.attackRange, Vector3.forward);
        // Explode
        // Cast rays all around the enemy
        foreach (RaycastHit2D hit in hits)
        {
            // Make sure there's actually something to hit
            if (hit.collider != null && hit.collider.gameObject != null && hit.collider.gameObject != gameObject)
            {
                // Get hit object
                GameObject hitObj = hit.collider.gameObject;

                // Check for player hit and deal damage
                if (hitObj.tag == "Player") {
                    hitObj.GetComponent<Health>().Damage(values.attackDamage);
                    continue;
                }

                // Check for enemy hit
                if (hitObj.layer == LayerMask.NameToLayer("Enemy")) {
                    hitObj.GetComponent<Health>().Damage(values.attackDamage);
                }
            }
        }
        Debug.Log("BOOM");
        // End attack
        gameObject.GetComponent<Health>().Damage(values.maxHealth);
    }
}
