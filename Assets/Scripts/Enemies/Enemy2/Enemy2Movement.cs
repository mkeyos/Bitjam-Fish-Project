using System;
using UnityEngine;

public class Enemy2Movement : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject player;

    [SerializeField] Enemy2Values values;

    [SerializeField] GameObject path;
    int currentPathNode = 0;

    public Vector2 lookDirection { get; private set; } = Vector2.down;

    enum moveStates
    {
        idle,
        path,
        chase,
        attack
    }

    moveStates currentState = moveStates.path;

    float currentSpeed = 0;
    Vector2  currentMovement = Vector2.zero;
    bool LOSToPlayer = false;
    bool playerSeen = false;
    float timeInLOS = 0;
    float pathReturnTimer = 1;
    Vector2 vectorTowardsPlayer;
    [HideInInspector] public bool inAttackRange = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
    }
    
    void Update()
    {
        PlayerStateCheck();
        Movement();
    }

    void PlayerStateCheck()
    {
        // Get vector towards player
        vectorTowardsPlayer = (player.transform.position - transform.position).normalized;

        // Raycast check
        RaycastHit2D hit = Physics2D.Raycast(transform.position, vectorTowardsPlayer, 100, values.raycastLayers);
        
        // Check angles to determine whether the player is in line of sight
        if (Vector2.Angle(lookDirection, vectorTowardsPlayer) < values.sightConeMaxAngle / 2
        && hit.distance < values.sightRange && hit.collider.gameObject.tag == "Player") { // In line of sight
            LOSToPlayer = true;
            timeInLOS += Time.deltaTime;
            pathReturnTimer = 1; // Reset path return timer
        }
        else { // NOT in line of sight
            LOSToPlayer = false;
            timeInLOS = 0;
        }

        // Determine whether the enemy is in attack distance to the player
        inAttackRange = (player.transform.position - transform.position).magnitude <= values.stoppingRange ? true : false;

        // Count down the time to return back on path after seeing the player briefly
        pathReturnTimer -= Time.deltaTime;

        // Determine if the enemy has had enough time to react to the player
        if (timeInLOS > values.reactionTime) playerSeen = true;
    }

    void Movement()
    {
        if (currentState == moveStates.idle)
        {
            // Decelerate existing movement
            Stop();

            // Start chase if the player enter field of view
            if (LOSToPlayer && timeInLOS > values.reactionTime) SetState(moveStates.chase);
            // Go back on path if the player left line of sight before reacting
            else if (!LOSToPlayer && !playerSeen && pathReturnTimer <= 0) SetState(moveStates.path);
        }
        else if (currentState == moveStates.path)
        {
            // Move towards the next path node
            Move(GetDirToPath(), values.pathSpeed);

            // Stop if in line of sigth to player or there path is not set
            if (LOSToPlayer || path == null) SetState(moveStates.idle);
        }
        else if (currentState == moveStates.chase)
        {
            // Move towards the player
            Move(vectorTowardsPlayer, values.chaseSpeed);

            // Go idle when line of sight gets broken
            if (!LOSToPlayer) SetState(moveStates.idle);
            else if (inAttackRange) SetState(moveStates.attack);
        }
        else if (currentState == moveStates.attack)
        {
            Stop();

            // Keep the player in sight
            lookDirection = vectorTowardsPlayer;

            // Start chasing if the player gets too far away to attack
            if (!inAttackRange) SetState(moveStates.chase);
            else if (!LOSToPlayer) SetState(moveStates.idle);
        }
    }

    void Move(Vector2 direction)
    {
        // Decelerate
        if (direction == Vector2.zero){
            currentSpeed -= Time.deltaTime * values.deceleration;
        }
        // Accelerate
        else {
            currentSpeed += Time.deltaTime * values.acceleration;
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0, values.chaseSpeed);

        // Movement direction
        currentMovement = Vector2.Lerp(currentMovement, direction, Time.deltaTime * values.turnSpeed);
        // Set look direction
        if (currentMovement != Vector2.zero) lookDirection = currentMovement;

        // Applying the movement to the rigidbody
        rb.linearVelocity = currentMovement * currentSpeed;
    }
    void Move(Vector2 direction, float speed)
    {
        // Decelerate
        if (direction == Vector2.zero){
            currentSpeed -= Time.deltaTime * values.deceleration;
        }
        // Accelerate
        else {
            currentSpeed += Time.deltaTime * values.acceleration;
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0, speed);

        // Movement direction
        currentMovement = Vector2.Lerp(currentMovement, direction, Time.deltaTime * values.turnSpeed);
        // Set look direction
        if (currentMovement != Vector2.zero) lookDirection = currentMovement;

        // Applying the movement to the rigidbody
        rb.linearVelocity = currentMovement * currentSpeed;
    }
    void Stop()
    {
        // Decelerate
        currentSpeed -= Time.deltaTime * values.deceleration;

        currentSpeed = Mathf.Clamp(currentSpeed, 0, values.chaseSpeed);

        // Movement direction
        currentMovement = Vector2.Lerp(currentMovement, Vector2.zero, Time.deltaTime * values.turnSpeed);

        // Applying the movement to the rigidbody
        rb.linearVelocity = currentMovement * currentSpeed;
    }

    Vector2 GetDirToPath()
    {
        // Return immediatly if the path is not set
        if (path == null) return Vector2.zero;
        else
        {
            // Get current destination path node position
            Vector3 pathNodePos = path.transform.GetChild(currentPathNode).transform.position;

            // Check if the enemy is close enough to target path node to continue on to the next
            if (Vector2.Distance(pathNodePos, transform.position) < 0.1)
            {
                currentPathNode++;
                // If the next node index exceeds path node amount, go back to 0
                if (currentPathNode > path.transform.childCount - 1) currentPathNode = 0;
            }
            
            // Return direction to the current path node
            return (pathNodePos - transform.position).normalized;
        }
    }

    void SetState(moveStates state) { currentState = state; }
}
