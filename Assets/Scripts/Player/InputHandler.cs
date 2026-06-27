using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] public PlayerController playerController;
    [SerializeField] private PlayerCameraManager playerCamera;
    [SerializeField] private GravityManager gravityManager;

    private InputAction _moveAction, _lookAction, _sprint, _crouch, _jumpAction;

    private void Start()
    {
        if (playerController == null) Debug.Log($"[{gameObject.name}] PlayerController is not assigned in the inspector.");
        if (playerCamera == null) Debug.Log($"[{gameObject.name}] PlayerCameraManager is not assigned in the inspector.");
        if (gravityManager == null) Debug.Log($"[{gameObject.name}] GravityManager is not assigned in the inspector.");

        _moveAction = InputSystem.actions.FindAction("Move");
        _lookAction = InputSystem.actions.FindAction("Look");
        _crouch = InputSystem.actions.FindAction("Crouch");
        _sprint = InputSystem.actions.FindAction("Sprint");

        _jumpAction = InputSystem.actions.FindAction("Jump");
        InputAction _toogleGravity = InputSystem.actions.FindAction("ToggleGravity");

        _jumpAction.performed += ctx => playerController.HandleJump();
        _toogleGravity.performed += ctx => gravityManager.ToggleGravity();
    }

    private void Update()
    {
        Vector2 lookVector = _lookAction.ReadValue<Vector2>();
        playerCamera.HandleHeadRotation(lookVector);

        Vector2 moveVector = _moveAction.ReadValue<Vector2>();
        playerController.HandleMovement(moveVector, _sprint.IsPressed(), _crouch.IsPressed());
    }
}
