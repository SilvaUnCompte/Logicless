using UnityEngine;

public class GravitySensor : MonoBehaviour
{
    [SerializeField] private float _sphereRadius = 0.5f;
    [SerializeField] private float _offsetFromCenter = 0.6f;
    [SerializeField] private LayerMask _groundLayer;

    public bool IsGrounded()
    {
        Vector3 spherePosition = transform.position + (transform.up * (GravityManager.gravity > 0 ? _offsetFromCenter : -_offsetFromCenter));
        return Physics.CheckSphere(spherePosition, _sphereRadius, _groundLayer);
    }

    
    private void OnDrawGizmos()
    {
        Gizmos.color = IsGrounded() ? Color.green : Color.red;
        Vector3 spherePosition = transform.position + (transform.up * (GravityManager.gravity > 0 ? _offsetFromCenter : -_offsetFromCenter));
        Gizmos.DrawWireSphere(spherePosition, _sphereRadius);
    }
}
