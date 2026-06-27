using UnityEngine;

public class PlayerCameraManager : MonoBehaviour
{
    [SerializeField] private float RotationSpeed = 20f;

    private float _rotationX = 0;
    private float _rotationY = 0;
    private float _rotationZ = 0;

    public void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void HandleHeadRotation(Vector2 rotationVector)
    {
        _rotationY += rotationVector.x * RotationSpeed * Time.deltaTime;
        _rotationX -= rotationVector.y * RotationSpeed * Time.deltaTime;

        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);

        transform.localEulerAngles = new Vector3(_rotationX, _rotationY, _rotationZ);
    }
}
