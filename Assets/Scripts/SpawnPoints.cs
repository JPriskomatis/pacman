using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class SpawnPoints : MonoBehaviour
{
    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private Tilemap groundTilemap;



    [SerializeField] private List<Vector3> locations = new List<Vector3>();

    private void Start()
    {
        for (int x = groundTilemap.cellBounds.xMin; x< groundTilemap.cellBounds.xMax; x++)
        {
            for (int y = groundTilemap.cellBounds.yMin; y < groundTilemap.cellBounds.yMax; y++)
            {
                Vector3Int localLocation = new Vector3Int(
                    x: x,
                    y: y,
                    z: 0);

                Vector3 location = groundTilemap.CellToWorld(localLocation);
                if (groundTilemap.HasTile(localLocation))
                {
                    locations.Add(location);
                }
            }
        }

        InvokeRepeating(
            methodName: "SpawnPoint",
            time: Random.Range(1,1),
            repeatRate: Random.Range(2, 5));
    }

    private void SpawnPoint()
    {
        int z = Random.Range(0, locations.Count);
        
        Instantiate(pointPrefab, new Vector2(
            x: locations[z].x+.5f,
            y: locations[z].y+.5f),
            Quaternion.identity);
    }

}
