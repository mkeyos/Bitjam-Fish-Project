using UnityEngine;

[CreateAssetMenu(fileName = "Enemy3Data", menuName = "menu/Enemy3Data")]
public class Enemy3Values : ScriptableObject
{
    [Header("Attack")]
    public LayerMask raycastLayers;
    public float targetingRange = 4;
    public float attackRange = 4;
    public float attackDamage = 1;
    public float attackStartDelay = 1;

    [Header("Health")]
    public float maxHealth = 1;
}
