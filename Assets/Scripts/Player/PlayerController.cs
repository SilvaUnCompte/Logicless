using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 8.0f;
    public float RotationSpeed = 20.0f;
    public float jumpForce = 10f;
    public float gravity = -35f;

    private CharacterController _characterController;

    private float _rotationY;
    private float _verticalVelocity;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public void HandleMovement(Vector2 moveVector)
    {
        if (_characterController.isGrounded && _verticalVelocity < 0) _verticalVelocity = -2f;

        Vector3 horizontalMove = (transform.right * moveVector.x + transform.forward * moveVector.y) * movementSpeed;
        _verticalVelocity += gravity * Time.deltaTime;
        Vector3 finalMove = new Vector3(horizontalMove.x, _verticalVelocity, horizontalMove.z);

        _characterController.Move(finalMove * Time.deltaTime);
    }

    public void HandleRotate(Vector2 rotationVector)
    {
        _rotationY += rotationVector.x * RotationSpeed * Time.deltaTime;
        transform.localEulerAngles = new Vector3(0, _rotationY, 0);
    }

    public void HandleJump()
    {
        if (_characterController.isGrounded)
        {
            _verticalVelocity = jumpForce;
        }
    }
}
