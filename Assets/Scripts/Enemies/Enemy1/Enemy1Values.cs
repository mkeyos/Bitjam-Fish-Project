using UnityEngine;

[CreateAssetMenu(fileName = "Enemy1Data", menuName = "menu/Enemy1Data")]
public class Enemy1Values : ScriptableObject
{
    [Header("Movement")]
    public float pathSpeed = 10;
    public float chaseSpeed = 10;
    public float acceleration = 1;
    public float deceleration = 1;
    public float turnSpeed = 10;

    [Header("Sight")]
    public LayerMask raycastLayers;
    public float sightConeMaxAngle = 90;
    public float sightRange = 10;
    public float reactionTime = 0.5f;

    [Header("Attack")]
    public float attackRange = 3;
    public float attackDamage = 1;

    [Header("Health")]
    public float maxHealth = 1;
}
