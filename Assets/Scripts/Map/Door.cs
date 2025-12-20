using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Door : MonoBehaviour
{
    public Vector3Int cell;
    public Tile tile;
    [SerializeField] private Tilemap doorTilemap;

    private IEnumerator Start()
    {
        //doorTilemap.SetColor(cell, Color.red);
        yield return new WaitForSeconds(2f);
        DisableDoor();
  
        
    }

    public void DisableDoor()
    {
        StopAllCoroutines();
        doorTilemap.SetTile(cell, null);
        StartCoroutine(EnableDoor());
    }

    public IEnumerator EnableDoor()
    {
        yield return new WaitForSeconds(5f);
        doorTilemap.SetTile(cell, tile);
    }

    private void OnDrawGizmos()
    {
        if (doorTilemap == null)
            return;

        // Draw left tunnel cell in green
        Vector3 leftPos = doorTilemap.GetCellCenterWorld(cell);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(leftPos, 0.2f);
        Gizmos.DrawLine(leftPos + Vector3.up * 0.3f, leftPos + Vector3.down * 0.3f);
        Gizmos.DrawLine(leftPos + Vector3.left * 0.3f, leftPos + Vector3.right * 0.3f);

        // Draw right tunnel cell in red
        Vector3 rightPos = doorTilemap.GetCellCenterWorld(cell);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(rightPos, 0.2f);
        Gizmos.DrawLine(rightPos + Vector3.up * 0.3f, rightPos + Vector3.down * 0.3f);
        Gizmos.DrawLine(rightPos + Vector3.left * 0.3f, rightPos + Vector3.right * 0.3f);
    }

}
