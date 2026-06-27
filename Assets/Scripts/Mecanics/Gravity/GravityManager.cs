using UnityEngine;

public class GravityManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private static float gravityScale = -1f;
    [SerializeField] private static float gravityStrength = 35f;

    public void ToggleGravity()
    {
        Debug.Log("Gravity toggled");
        Physics.gravity = -Physics.gravity;
        gravityScale *= -1;
    }

    public static float gravity => gravityScale * gravityStrength;
}
