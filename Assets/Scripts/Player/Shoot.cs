using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public SpriteRenderer gunSpriteRenderer;
    private Animator anim;
    private Vector3 originalFirePointPos;

    public float bulletSpeed = 20f;

    void Start()
    {
        originalFirePointPos = firePoint.localPosition;
        anim = GetComponent<Animator>();
    }

    public void OnAttack()
    {
        ShootGun();
    }

    void ShootGun()
    {
        anim.SetTrigger("Shoot");
        if (gunSpriteRenderer.flipY)
        {
            firePoint.localPosition = new Vector3(
                originalFirePointPos.x,
                -originalFirePointPos.y,
                originalFirePointPos.z
            );
        }
        else
        {
            firePoint.localPosition = originalFirePointPos;
        }


        GameObject bullet = Instantiate(
            bulletPrefab, 
            firePoint.position, 
            firePoint.rotation
        );

        SpriteRenderer bulletSprite = bullet.GetComponentInChildren<SpriteRenderer>();
        bulletSprite.flipY = gunSpriteRenderer.flipY;

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletSpeed, ForceMode2D.Impulse);
    }
}
