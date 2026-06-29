using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] public PlayerController playerController;
    [SerializeField] private PlayerCameraManager playerCamera;
    [SerializeField] private PlayerStat playerStat;
    [SerializeField] private GravityManager gravityManager;
    [SerializeField] private UIManager uiManager;

    private InputAction _moveAction, _lookAction, _sprint, _crouch;

    private void Start()
    {
        if (playerController == null) Debug.LogError($"[{gameObject.name}] PlayerController is not assigned in the inspector.");
        if (playerCamera == null) Debug.LogError($"[{gameObject.name}] PlayerCameraManager is not assigned in the inspector.");
        if (gravityManager == null) Debug.LogError($"[{gameObject.name}] GravityManager is not assigned in the inspector.");
        if (playerStat == null) Debug.LogError($"[{gameObject.name}] PlayerStat is not assigned in the inspector.");
        if (uiManager == null) Debug.LogError($"[{gameObject.name}] UIManager is not assigned in the inspector.");

        _moveAction = InputSystem.actions.FindAction("Move");
        _lookAction = InputSystem.actions.FindAction("Look");
        _crouch = InputSystem.actions.FindAction("Crouch");
        _sprint = InputSystem.actions.FindAction("Sprint");

        InputAction jumpAction = InputSystem.actions.FindAction("Jump");
        InputAction toogleGravity = InputSystem.actions.FindAction("ToggleGravity");
        InputAction pause = InputSystem.actions.FindAction("Pause");

        jumpAction.performed += ctx => playerController.HandleJump();
        toogleGravity.performed += ctx => gravityManager.ToggleGravity();
        pause.performed += ctx => uiManager.EschapPerformed();
    }

    private void Update()
    {
        playerStat.isSprinting = _sprint.IsPressed();
        playerStat.isCrouching = _crouch.IsPressed();

        Vector2 lookVector = _lookAction.ReadValue<Vector2>();
        playerCamera.HandleHeadRotation(lookVector);

        Vector2 moveVector = _moveAction.ReadValue<Vector2>();
        playerController.HandleMovement(moveVector);
    }
}
