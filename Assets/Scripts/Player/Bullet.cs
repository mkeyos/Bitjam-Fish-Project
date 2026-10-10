using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;
    public float maxRange = 50;

    [HideInInspector] public Vector2 startPosition { get; private set; }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
        Destroy(gameObject);

        // Deal damage to whatever
        if (collision.gameObject.GetComponent<Health>()) collision.gameObject.GetComponent<Health>().Damage(1);
    }

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Kill the bullet upon reaching max range
        if (Vector2.Distance(startPosition, transform.position) >= maxRange)
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
            Destroy(gameObject);
        }
    }
}
