using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Vector3Int leftTunnelCell;
    [SerializeField] private Vector3Int rightTunnelCell;
    [SerializeField] private GameObject player;

    [SerializeField] private BoxCollider2D leftBoxCollider;
    [SerializeField] private BoxCollider2D rightBoxCollider;


    [SerializeField] private bool isLeftTunnel = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        Vector3Int targetCell = isLeftTunnel ? rightTunnelCell : leftTunnelCell;
        Vector3 targetWorldPos = tilemap.GetCellCenterWorld(targetCell);

        //Moves the player to the exact center of the target cell
        other.transform.position = targetWorldPos;

        StartCoroutine(DisableColliderTemporarily(isLeftTunnel ? rightBoxCollider : leftBoxCollider));


        PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.TeleportToCell(targetCell);
        }


        
    }

    private IEnumerator DisableColliderTemporarily(BoxCollider2D collider)
    {
        collider.enabled = false;
        yield return new WaitForSeconds(0.2f);
        collider.enabled = true;
    }


    // Draw Gizmos in the Scene view
    private void OnDrawGizmos()
    {
        if (tilemap == null)
            return;

        // Draw left tunnel cell in green
        Vector3 leftPos = tilemap.GetCellCenterWorld(leftTunnelCell);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(leftPos, 0.2f);
        Gizmos.DrawLine(leftPos + Vector3.up * 0.3f, leftPos + Vector3.down * 0.3f);
        Gizmos.DrawLine(leftPos + Vector3.left * 0.3f, leftPos + Vector3.right * 0.3f);

        // Draw right tunnel cell in red
        Vector3 rightPos = tilemap.GetCellCenterWorld(rightTunnelCell);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(rightPos, 0.2f);
        Gizmos.DrawLine(rightPos + Vector3.up * 0.3f, rightPos + Vector3.down * 0.3f);
        Gizmos.DrawLine(rightPos + Vector3.left * 0.3f, rightPos + Vector3.right * 0.3f);
    }
}
