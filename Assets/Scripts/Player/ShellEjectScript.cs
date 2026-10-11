using UnityEngine;

public class ShellEjectScript : MonoBehaviour
{
    public Transform shell;
    
    // Ejection
    public float ejectionForce;
    public float velocityUp;
    public float gravity;

    public float rotationSpeed; // Degrees per second

    private Vector2 groundVelocity;
    private float height;
    private float heightVelocity;
    private bool landed = false;
    private float currRotationSpeed;
    void Start()
    {
        
    }

    public void Eject(Vector2 gunFoward)
    {
        Vector2 ejectDirection = new Vector2(-gunFoward.y, gunFoward.x);

        ejectDirection *= Random.value < 0.5f ? 1f : -1f; // Randomly flip the direction
        groundVelocity = ejectDirection * ejectionForce;
        heightVelocity = velocityUp;

        currRotationSpeed = Mathf.Abs(rotationSpeed);
        currRotationSpeed *= Random.value < 0.5f ? 1f : -1f; // Randomly flip the spin direction
    }

    void Update()
    {
        transform.position += (Vector3)groundVelocity * Time.deltaTime;

        if (!landed)
        {
            heightVelocity -= gravity * Time.deltaTime;
            height += heightVelocity * Time.deltaTime;

            if (height <= 0)
            {
                height = 0;
                landed = true;
                groundVelocity *= 0f;
            }
        }

        Vector3 visualPosition = shell.localPosition;
        visualPosition.y = height;
        shell.localPosition = visualPosition;

        if (!landed)
        {
            shell.localRotation *= Quaternion.Euler(0f, 0f, currRotationSpeed * Time.deltaTime);
        }
    }
}
