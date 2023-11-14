using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public static PlayerInput PlayerInput;
    public bool isPressedTouch { get; private set; }
    public Vector2 positionTouch { get; private set; }
    private InputAction _pressTouch;
    private InputAction _moveTouch;

    private void Awake() {
        if (instance == null)
        {
            instance = this;
        }

        PlayerInput = GetComponent<PlayerInput>();
        _pressTouch = PlayerInput.actions["PressTouch"];
        _moveTouch = PlayerInput.actions["MoveTouch"];
    }

    private void Update() {
        isPressedTouch = _pressTouch.WasPerformedThisFrame();
        positionTouch = _moveTouch.ReadValue<Vector2>();
    }
}
