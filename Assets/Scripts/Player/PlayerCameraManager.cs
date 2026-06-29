using UnityEngine;

public class PlayerCameraManager : MonoBehaviour
{
    [SerializeField] private float RotationSpeed = 20f;
    [SerializeField] private PlayerStat playerStat;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private PortalTextureSetup portalTextureSetup;
    [SerializeField] private CharacterController characterController;

    [Header("Camera Settings")]
    [SerializeField] private float baseFOV = 60f;
    [SerializeField] private float sprintFOV = 70f;
    [SerializeField] private float fovSpeed = 5f;
    [SerializeField] private float standHeight = 0.5f;
    [SerializeField] private float crouchHeight = 0.25f;
    [SerializeField] private float crouchTransitionSpeed = 6f;

    private float _rotationX = 0;
    private float _rotationY = 0;
    private float _rotationZ = 0;

    public void Start()
    {
        if (playerStat == null) Debug.LogError($"[{gameObject.name}] PlayerStat is not assigned in the inspector.");
        if (playerCamera == null) Debug.LogError($"[{gameObject.name}] Camera is not assigned in the inspector.");
        if (portalTextureSetup == null) Debug.LogError($"[{gameObject.name}] PortalTextureSetup is not assigned in the inspector.");
        if (characterController == null) Debug.LogError($"[{gameObject.name}] CharacterController is not assigned in the inspector.");
    }

    public void HandleHeadRotation(Vector2 rotationVector)
    {
        _rotationY += rotationVector.x * RotationSpeed * Time.deltaTime;
        _rotationX -= rotationVector.y * RotationSpeed * Time.deltaTime;

        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);

        transform.localEulerAngles = new Vector3(_rotationX, _rotationY, _rotationZ);
    }

    public void Update()
    {
        float targetFOV = (playerStat.isSprinting && !playerStat.isCrouching && characterController.velocity.magnitude > 0.5) ? sprintFOV : baseFOV;

        foreach (PortalTextureSetupData SetupData in portalTextureSetup.portalTextureSetupDataList)
        {
            SetupData.camera.fieldOfView = Mathf.Lerp(
                playerCamera.fieldOfView,
                targetFOV,
                Time.deltaTime * fovSpeed
            );
        }

        playerCamera.fieldOfView = Mathf.Lerp(
            playerCamera.fieldOfView,
            targetFOV,
            Time.deltaTime * fovSpeed
        );

        float targetY = playerStat.isCrouching ? crouchHeight : standHeight;
        Vector3 localPos = transform.localPosition;
        localPos.y = Mathf.Lerp(localPos.y, targetY, Time.deltaTime * crouchTransitionSpeed);
        transform.localPosition = localPos;
    }
}
