using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 5.5f;
    [SerializeField] private float sprintMultiplier = 1.5f;
    [SerializeField] private float crouchMultiplier = 0.35f;
    [SerializeField] private float jumpForce = 10f;

    [Header("References")]
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private GravitySensor gravitySensor;
    [SerializeField] private GravityManager gravityManager;
    [SerializeField] private PlayerCameraManager playerCameraManager;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private PlayerStat playerStat;

    private float _verticalVelocity;


    void Start()
    {
        if (_characterController == null) Debug.LogError($"[{gameObject.name}] CharacterController is not assigned in the inspector.");
        if (gravitySensor == null) Debug.LogError($"[{gameObject.name}] GravitySensor is not assigned in the inspector.");
        if (gravityManager == null) Debug.LogError($"[{gameObject.name}] GravityManager is not assigned in the inspector.");
        if (playerCameraManager == null) Debug.LogError($"[{gameObject.name}] PlayerCameraManager is not assigned in the inspector.");
        if (cameraTransform == null) Debug.LogError($"[{gameObject.name}] Camera Transform is not assigned in the inspector.");
        if (playerStat == null) Debug.LogError($"[{gameObject.name}] PlayerStat is not assigned in the inspector.");
    }
    
    public void HandleMovement(Vector2 moveVector)
    {
        float actualSpeed = movementSpeed * (playerStat.isSprinting ? sprintMultiplier : 1) * (playerStat.isCrouching ? crouchMultiplier : 1);

        // Based on the camera's orientation
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 horizontalMove = (camRight * moveVector.x + camForward * moveVector.y) * actualSpeed;

        // Gravity handling
        if (gravitySensor.IsGrounded() && (_verticalVelocity * GravityManager.gravity > 0)) _verticalVelocity = (GravityManager.gravity > 0) ? 2f : -2f;
        _verticalVelocity += GravityManager.gravity * Time.deltaTime;

        // Apply movement
        Vector3 finalMove = new Vector3(horizontalMove.x, _verticalVelocity, horizontalMove.z);
        _characterController.Move(finalMove * Time.deltaTime);
    }
    
    public void HandleJump()
    {
        if (gravitySensor.IsGrounded()) _verticalVelocity = jumpForce * -GravityManager.gravityScaleValue;
    }
}
