using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 5.5f;
    [SerializeField] private float sprintMultiplier = 1.5f;
    [SerializeField] private float crouchMultiplier = 0.35f;

    [Header("References")]
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private GravitySensor gravitySensor;
    [SerializeField] private GravityManager gravityManager;
    [SerializeField] private PlayerCameraManager playerCameraManager;

    [Header("Other Settings")]
    [SerializeField] private float jumpForce = 10f;

    private float _verticalVelocity;


    void Start()
    {
        if (_characterController == null) Debug.LogError($"[{gameObject.name}] CharacterController is not assigned in the inspector.");
        if (gravitySensor == null) Debug.LogError($"[{gameObject.name}] GravitySensor is not assigned in the inspector.");
        if (gravityManager == null) Debug.LogError($"[{gameObject.name}] GravityManager is not assigned in the inspector.");
        if (playerCameraManager == null) Debug.LogError($"[{gameObject.name}] PlayerCameraManager is not assigned in the inspector.");
    }

    public void HandleMovement(Vector2 moveVector, bool isSprinting, bool isCrouching) // In Update
    {
        float actualSpeed = movementSpeed * (isSprinting ? sprintMultiplier : 1) * (isCrouching ? crouchMultiplier : 1);
        Vector3 horizontalMove = (transform.right * moveVector.x + transform.forward * moveVector.y) * actualSpeed;

        // Reset vertical velocity if grounded and moving in the direction of gravity
        if (gravitySensor.IsGrounded() && (_verticalVelocity * GravityManager.gravity > 0)) _verticalVelocity = (GravityManager.gravity > 0) ? 2f : -2f;

        _verticalVelocity += GravityManager.gravity * Time.deltaTime;
        Vector3 finalMove = new Vector3(horizontalMove.x, _verticalVelocity, horizontalMove.z);

        _characterController.Move(finalMove * Time.deltaTime);
    }

    public void HandleJump()
    {
        if (gravitySensor.IsGrounded()) _verticalVelocity = jumpForce;
        Debug.Log($"Jumping! Grounded: {gravitySensor.IsGrounded()}, Vertical Velocity: {_verticalVelocity}");
    }
}
