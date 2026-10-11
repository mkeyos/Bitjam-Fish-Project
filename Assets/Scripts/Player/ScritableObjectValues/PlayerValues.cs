using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "menu/PlayerData")]
public class PlayerValues : ScriptableObject
{
    [Header("Movement")]
    [SerializeField] public float maxSpeed = 10;
    public float currentSpeed = 0;
    public Vector2  currentMovement = Vector2.zero;
    [SerializeField] public float acceleration = 1;
    [SerializeField] public float deceleration = 1;
    [SerializeField] public float turnSpeed = 10;
    [Header("Camera")]
    [SerializeField] public float cameraSpeed = 10;
    [SerializeField] public float camMaxDistFromPlayer = 5;
}
