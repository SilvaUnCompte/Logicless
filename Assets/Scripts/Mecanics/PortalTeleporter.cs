using UnityEngine;


public enum PortalType
{
    ViewBased,
    MovementBased,
    Both,
    Neither
}

public class PortalTeleporter : MonoBehaviour
{
    public Transform player;
    public Transform reciever;
    public PortalType PortalType;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Vector3 portalDirection = transform.up;
            Vector3 playerMovementDirection = other.GetComponent<CharacterController>().velocity.normalized;
            Vector3 playerViewDirection = other.transform.forward.normalized;
            float movementDot = Vector3.Dot(portalDirection, playerMovementDirection);
            float viewDot = Vector3.Dot(portalDirection, playerViewDirection);

            switch (PortalType)
            {
                case PortalType.ViewBased:
                    if (viewDot < 0) TeleportPlayer(other);
                    break;
                case PortalType.MovementBased:
                    if (movementDot < 0) TeleportPlayer(other);
                    break;
                case PortalType.Both:
                    if (viewDot < 0 && movementDot < 0) TeleportPlayer(other);
                    break;
                case PortalType.Neither:
                    TeleportPlayer(other);
                    break;
            }
        }
    }

    private void TeleportPlayer(Collider playerCollider)
    {
        Debug.Log("Player has moved across the portal named " + gameObject.name);

        float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);
        player.Rotate(Vector3.up, rotationDiff += 180);

        Vector3 portalToPlayer = player.position - transform.position;
        Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;

        player.position = reciever.position + positionOffset;
        Physics.SyncTransforms();
    }
}
