using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
        Destroy(gameObject);

        // Deal damage to whatever
        if (collision.gameObject.GetComponent<Health>()) collision.gameObject.GetComponent<Health>().Damage(1);
    }
}
