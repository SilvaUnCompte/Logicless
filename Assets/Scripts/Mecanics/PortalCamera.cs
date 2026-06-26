using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public Transform playerCamera;
    public Transform portalIn;
    public Transform otherOut;

    void LateUpdate()
    {
        Vector3 playerOffsetFromPortal = playerCamera.position - portalIn.position;
        transform.position = otherOut.position + playerOffsetFromPortal;

        float angularDifferenceBetweenPortalRotations = Quaternion.Angle(portalIn.rotation, otherOut.rotation);
        Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
        Vector3 newCameraDirection = portalRotationalDifference * playerCamera.forward;
        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
    }
}
