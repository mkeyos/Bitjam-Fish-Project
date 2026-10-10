using UnityEngine;

[CreateAssetMenu(fileName = "Enemy2Data", menuName = "menu/Enemy2Data")]
public class Enemy2Values : ScriptableObject
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
    public float stoppingRange = 3;
    public float attackRange = 4;
    public float attackSpread = 90;
    public float attackDamage = 1;
    public float attackStartDelay = 1;
    public float attackCooldown = 2;

    [Header("Health")]
    public float maxHealth = 1;
}
