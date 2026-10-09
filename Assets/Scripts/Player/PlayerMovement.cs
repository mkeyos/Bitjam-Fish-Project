using Unity.Collections;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerMovement : MonoBehaviour
{
    
    
    [SerializeField] private PlayerValues playerValues;

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
        // Accelerate
        if (input.move == Vector2.zero){
            playerValues.currentSpeed -= Time.fixedDeltaTime * playerValues.deceleration;
        }
        // Decelerate
        else {
            playerValues.currentSpeed += Time.fixedDeltaTime * playerValues.acceleration;
        }
        playerValues.currentSpeed = Mathf.Clamp(playerValues.currentSpeed, 0, playerValues.maxSpeed);

        // Movement direction
        playerValues.currentMovement = Vector2.Lerp(playerValues.currentMovement, input.move, Time.fixedDeltaTime * playerValues.turnSpeed);

        // Applying the movement to the rigidbody
        rb.linearVelocity = playerValues.currentMovement * playerValues.currentSpeed;
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

        Vector2 camDestination = pPos + (camDestinationFromPlayer < playerValues.camMaxDistFromPlayer ? fromPlayerToDestination : fromPlayerToDestination.normalized*playerValues.camMaxDistFromPlayer);

        Vector2 camPosition = Vector2.Lerp(Camera.main.transform.position, camDestination, playerValues.cameraSpeed/10);
        Camera.main.transform.position = new Vector3(camPosition.x, camPosition.y, -10);
    }
}
