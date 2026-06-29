using UnityEngine;

public class PlayerCameraManager : MonoBehaviour
{
    [SerializeField] private float RotationSpeed = 20f;
    [SerializeField] private PlayerStat playerStat;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private PortalTextureSetup portalTextureSetup;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private GravitySensor gravitySensor;

    [Header("Crouch Settings")]
    [SerializeField] private float standHeight = 0.5f;
    [SerializeField] private float crouchHeight = 0.25f;
    [SerializeField] private float crouchTransitionSpeed = 6f;

    [Header("FOV Settings")]
    [SerializeField] private float baseFOV = 60f;
    [SerializeField] private float sprintFOV = 70f;
    [SerializeField] private float fovSpeed = 5f;

    [Header("Head Bob Settings")]
    [SerializeField] private float bobSpeed = 14f;
    [SerializeField] private float bobAmount = 0.01f;

    private float _rotationX = 0;
    private float _rotationY = 0;
    private float _rotationZ = 0;

    private float _bobTimer;

    public void Start()
    {
        if (playerStat == null) Debug.LogError($"[{gameObject.name}] PlayerStat is not assigned in the inspector.");
        if (playerCamera == null) Debug.LogError($"[{gameObject.name}] Camera is not assigned in the inspector.");
        if (portalTextureSetup == null) Debug.LogError($"[{gameObject.name}] PortalTextureSetup is not assigned in the inspector.");
        if (characterController == null) Debug.LogError($"[{gameObject.name}] CharacterController is not assigned in the inspector.");
        if (gravitySensor == null) Debug.LogError($"[{gameObject.name}] GravitySensor is not assigned in the inspector.");
    }

    public void Update()
    {
        // --- FOV ---
        float targetFOV = (playerStat.isSprinting && !playerStat.isCrouching && characterController.velocity.magnitude > 0.1f) ? sprintFOV : baseFOV;

        foreach (PortalTextureSetupData SetupData in portalTextureSetup.portalTextureSetupDataList)
        {
            SetupData.camera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, targetFOV, Time.deltaTime * fovSpeed);
        }
        playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, targetFOV, Time.deltaTime * fovSpeed);

        // --- Crouch Transition ---
        float targetY = playerStat.isCrouching ? crouchHeight : standHeight;
        float currentY = Mathf.Lerp(transform.localPosition.y, targetY, Time.deltaTime * crouchTransitionSpeed);

        // --- Head Bob ---
        if (gravitySensor.IsGrounded() && characterController.velocity.magnitude > 0.1f)
        {
            float currentBobAmount = (playerStat.isCrouching) ? bobAmount * .1f : bobAmount;
            float currentBobSpeed = (playerStat.isCrouching) ? bobSpeed * .5f : bobSpeed;

            _bobTimer += Time.deltaTime * (playerStat.isSprinting ? currentBobSpeed * 1.5f : currentBobSpeed);
            float bobY = Mathf.Sin(_bobTimer) * currentBobAmount * 0.8f;
            float bobX = Mathf.Cos(_bobTimer / 2f) * currentBobAmount * 5f;
            Vector3 bobOffset = transform.localRotation * new Vector3(bobX, bobY, 0f);

            transform.localPosition = new Vector3(0, currentY, 0) + bobOffset;
        }
        else
        {
            _bobTimer = 0;
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, currentY, 0), Time.deltaTime * crouchTransitionSpeed);
        }
    }

    public void HandleHeadRotation(Vector2 rotationVector)
    {
        _rotationY += rotationVector.x * RotationSpeed * Time.deltaTime;
        _rotationX -= rotationVector.y * RotationSpeed * Time.deltaTime;

        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);

        transform.localEulerAngles = new Vector3(_rotationX, _rotationY, _rotationZ);
    }
}
