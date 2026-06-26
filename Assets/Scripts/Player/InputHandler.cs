using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public PlayerController playerController;
    private InputAction _moveAction, _lookAction, _jumpAction;

    private void Start()
    {
        Cursor.visible = false;

        _moveAction = InputSystem.actions.FindAction("Move");
        _lookAction = InputSystem.actions.FindAction("Look");
        _jumpAction = InputSystem.actions.FindAction("Jump");

        _jumpAction.performed += ctx => playerController.HandleJump();
    }

    private void Update()
    {
        Vector2 lookVector = _lookAction.ReadValue<Vector2>();
        playerController.HandleRotate(lookVector);
        Vector2 moveVector = _moveAction.ReadValue<Vector2>();
        playerController.HandleMovement(moveVector);
    }
}
