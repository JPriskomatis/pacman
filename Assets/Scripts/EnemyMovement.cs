using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;          // Enemy movement speed
    public Tilemap wallsTilemap;          // Reference to your Walls Tilemap

    private Vector3 targetPosition;       // Current target position
    private Vector2 currentDirection;     // Current moving direction

    [SerializeField] private SpriteRenderer image;
    [SerializeField] private Sprite[] logos;
    private void Start()
    {
        int randomLogo = Random.Range(0, logos.Length);
        image.sprite = logos[randomLogo];
        // Snap enemy to the center of its starting tile
        targetPosition = wallsTilemap.GetCellCenterWorld(wallsTilemap.WorldToCell(transform.position));
        transform.position = targetPosition;

        // Start with a random direction
        currentDirection = GetRandomDirection();
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        if ((Vector3)transform.position == targetPosition)
        {
            // At intersections, choose a new direction
            List<Vector2> validDirections = GetValidDirections();

            if (validDirections.Count > 0)
            {
                // Pick a random valid direction that isn’t the exact opposite
                Vector2 newDirection;
                do
                {
                    newDirection = validDirections[Random.Range(0, validDirections.Count)];
                } while (newDirection == -currentDirection && validDirections.Count > 1);

                currentDirection = newDirection;
            }

            // Set target to the center of the next tile
            Vector3Int targetCell = wallsTilemap.WorldToCell(transform.position + (Vector3)currentDirection);
            targetPosition = wallsTilemap.GetCellCenterWorld(targetCell);
        }

        // Smooth movement
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    // Returns all valid directions the enemy can move in from current position
    List<Vector2> GetValidDirections()
    {
        List<Vector2> directions = new List<Vector2>();

        Vector2[] possibleDirs = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        foreach (Vector2 dir in possibleDirs)
        {
            Vector3Int checkCell = wallsTilemap.WorldToCell(transform.position + (Vector3)dir);
            if (!wallsTilemap.HasTile(checkCell))
                directions.Add(dir);
        }

        return directions;
    }

    // Returns a random direction that isn’t blocked
    Vector2 GetRandomDirection()
    {
        List<Vector2> dirs = GetValidDirections();
        if (dirs.Count == 0)
            return Vector2.zero;

        return dirs[Random.Range(0, dirs.Count)];
    }
}
