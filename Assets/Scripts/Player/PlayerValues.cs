using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "menu/PlayerData")]
public class PlayerValues : ScriptableObject
{
    #region MovementSettings
    [Header("Movement")]
    public float maxSpeed = 10; 
    public float currentSpeed = 0;
    public Vector2  currentMovement = Vector2.zero;
    public float acceleration = 1;
    public float deceleration = 1;
    public float turnSpeed = 10;
    #endregion
    
    #region CameraSettings
    [Header("Camera")]
    public float cameraSpeed = 10;
    public float camMaxDistFromPlayer = 5;
    #endregion

    [Header("Health")]
    public float maxHealth = 1;
}
