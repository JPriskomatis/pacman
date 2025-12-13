using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnPoints : MonoBehaviour
{
    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private GameObject powerUpPrefab;
    [SerializeField] private Tilemap groundTilemap;


    private void Start()
    {
        // Spawn points on every tile (your existing logic)
        for (int x = groundTilemap.cellBounds.xMin; x < groundTilemap.cellBounds.xMax; x++)
        {
            for (int y = groundTilemap.cellBounds.yMin; y < groundTilemap.cellBounds.yMax; y++)
            {
                Vector3Int cell = new Vector3Int(x, y, 0);

                if (!groundTilemap.HasTile(cell))
                    continue;

                Vector3 worldPos = groundTilemap.GetCellCenterWorld(cell);
                Instantiate(pointPrefab, worldPos, Quaternion.identity);
            }
        }


    }
}
