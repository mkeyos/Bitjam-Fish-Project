using Unity.VisualScripting;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject muzzleFlashPrefab;
    public SpriteRenderer gunSpriteRenderer;
    private Animator anim;
    private Vector3 originalFirePointPos;

    public float bulletSpeed = 20f;

    // Shell Ejection
    public Transform shellEjectPoint;
    public GameObject shellPrefab;

    void Start()
    {
        originalFirePointPos = firePoint.localPosition;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootGun();
        }
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

        GameObject flash = Instantiate(
            muzzleFlashPrefab, 
            firePoint.position, 
            firePoint.rotation
        );

        SpriteRenderer flashSprite = flash.GetComponentInChildren<SpriteRenderer>();
        flashSprite.flipY = gunSpriteRenderer.flipY;
        Destroy(flash, 1f);

        GameObject bullet = Instantiate(
            bulletPrefab, 
            firePoint.position, 
            firePoint.rotation
        );

        GameObject shell = Instantiate(
            shellPrefab, 
            shellEjectPoint.position, 
            Quaternion.identity
        );

        ShellEjectScript shellEjectScript = shell.GetComponent<ShellEjectScript>();
        SpriteRenderer shellSprite = shell.GetComponentInChildren<SpriteRenderer>();
        shellSprite.flipX = gunSpriteRenderer.flipY;
        shellEjectScript.Eject(firePoint.right);
        Destroy(shell, 5f);

        SpriteRenderer bulletSprite = bullet.GetComponentInChildren<SpriteRenderer>();
        bulletSprite.flipY = gunSpriteRenderer.flipY;

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletSpeed, ForceMode2D.Impulse);
    }
}
