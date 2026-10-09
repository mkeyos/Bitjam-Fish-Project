using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public Vector2 move { get; private set; }
    public Vector2 cursorPosition { get; private set; }
    public int attack { get; private set; }

    void Update()
    {
        // Get cursor world position
        cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnMove(InputValue inputValue)
    {
        Vector2 input = inputValue.Get<Vector2>();
        move = input;
    }
}
