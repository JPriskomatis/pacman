using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    private Enemy enemy;

    public float moveSpeed = 3f;
    private float originalMoveSpeed;
    public Tilemap wallsTilemap;

    private Vector3 targetPosition;
    private Vector2 currentDirection;


    private bool canMove = true;

    public GameEvent EnemyDeath;

    private void Start()
    {
        originalMoveSpeed = moveSpeed;
        enemy = GetComponent<Enemy>();
        wallsTilemap = GameObject.FindGameObjectWithTag("WallGrid").GetComponent<Tilemap>();

        

        targetPosition = wallsTilemap.GetCellCenterWorld(wallsTilemap.WorldToCell(transform.position));
        transform.position = targetPosition;

        currentDirection = GetRandomDirection();
    }

    private void Update()
    {
        if (canMove)
        {
            Move();
            enemy.SetDirection(currentDirection, (Vector3)transform.position != targetPosition);

        }

    }

    void Move()
    {
        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // When we reach target tile center
        if ((Vector3)transform.position == targetPosition)
        {
            // Get all valid directions
            List<Vector2> validDirections = GetValidDirections();

            if (validDirections.Count == 0)
            {
                // No valid moves? Stay put
                currentDirection = Vector2.zero;
                return;
            }

            // If we're in a corridor (2 valid directions and one is reverse), continue forward mostly
            if (validDirections.Count == 2 && validDirections.Contains(-currentDirection))
            {
                // 80% chance to continue forward, 20% chance to turn
                if (Random.value < 0.8f)
                {
                    validDirections.Remove(-currentDirection);
                }
            }
            else if (validDirections.Count > 1)
            {
                // Avoid going backward if possible
                validDirections.Remove(-currentDirection);
            }

            // Pick new direction randomly from remaining options
            currentDirection = validDirections[Random.Range(0, validDirections.Count)];
            //enemy.SetDirection(currentDirection, true);


            // Set next target position
            Vector3Int targetCell = wallsTilemap.WorldToCell(transform.position + (Vector3)currentDirection);
            targetPosition = wallsTilemap.GetCellCenterWorld(targetCell);
        }
    }

    List<Vector2> GetValidDirections()
    {
        List<Vector2> directions = new List<Vector2>();
        Vector2[] possibleDirs = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

        foreach (Vector2 dir in possibleDirs)
        {
            Vector3Int checkCell = wallsTilemap.WorldToCell(transform.position + (Vector3)dir);
            if (!wallsTilemap.HasTile(checkCell))
            {
                directions.Add(dir);
            }
        }

        return directions;
    }

    Vector2 GetRandomDirection()
    {
        List<Vector2> dirs = GetValidDirections();
        if (dirs.Count == 0) return Vector2.zero;
        return dirs[Random.Range(0, dirs.Count)];
    }

    public void StopMovement()
    {
        canMove = false;
        enemy.SetDirection(currentDirection, false);

    }

    public void SetSpeed(int speed)
    {
        moveSpeed = speed;
    }

    public void ResetSpeed()
    {
        moveSpeed = originalMoveSpeed;
    }


}

