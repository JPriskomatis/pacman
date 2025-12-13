using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    [SerializeField] private Tilemap grid;

    [SerializeField] private Vector3Int spawnEnemyPosition = new Vector3Int(3, 3, 3);

    [SerializeField] private int spawnEnemiesCount = 4;

    Vector3 worldPos;


    private void Start()
    {
        SpawnEnemyPosition();
    }

    private void SpawnEnemyPosition()
    {
        if (grid.HasTile(spawnEnemyPosition))
        {
            worldPos = grid.GetCellCenterWorld(spawnEnemyPosition);

            for (int i = 0; i < spawnEnemiesCount; i++)
            {
                Instantiate(enemy, worldPos, Quaternion.identity);
            }
            spawnEnemiesCount = 1;
            
        }
        else
        {
            Debug.LogWarning("No tile at {cell}");
        }
    }

    public void StartSpawnEnemy()
    {
        StartCoroutine(SpawnNewEnemy());
    }
    IEnumerator SpawnNewEnemy()
    {
        yield return new WaitForSeconds(Random.Range(0, 10f));
        Instantiate(enemy, worldPos, Quaternion.identity);
    }

}
