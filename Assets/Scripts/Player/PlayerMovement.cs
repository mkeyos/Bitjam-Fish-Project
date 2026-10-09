using Unity.Collections;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float maxSpeed = 10;
    float currentSpeed = 0;
    Vector2 currentMovement = Vector2.zero;
    [SerializeField] float accelerationTime = 1;
    [SerializeField] float turnSpeed = 10;
    [Header("Camera")]
    [SerializeField] float cameraSpeed = 10;
    [SerializeField] float camMaxDistFromPlayer = 5;

    Rigidbody2D rb;
    PlayerInput input;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        MoveCamera();
    }

    void Move()
    {
        rb.linearVelocity = input.move * maxSpeed;
    }

    void MoveCamera()
    {
        // Cursos position
        Vector2 cPos = input.cursorPosition;
        // Player position
        Vector2 pPos = transform.position;
        Vector2 fromPlayerToCursor = cPos - pPos;
        Vector2 fromPlayerToDestination = (cPos - pPos)/2;
        float cursorDistFromPlayer = fromPlayerToCursor.magnitude;
        float camDestinationFromPlayer = fromPlayerToCursor.magnitude/2;

        Vector2 camDestination = pPos + (camDestinationFromPlayer < camMaxDistFromPlayer ? fromPlayerToDestination : fromPlayerToDestination.normalized*camMaxDistFromPlayer);

        Vector2 camPosition = Vector2.Lerp(Camera.main.transform.position, camDestination, cameraSpeed/10);
        Camera.main.transform.position = new Vector3(camPosition.x, camPosition.y, -10);
    }
}
