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

    public Transform player;

    float chaseChance = 0.65f;



    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
        
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        //when we reach target tile center;
        if ((Vector3)transform.position == targetPosition)
        {
            List<Vector2> validDirections = GetValidDirections();

            if (validDirections.Count == 0)
            {
                currentDirection = Vector2.zero;
                return;
            }

            if (validDirections.Count > 1)
            {
                validDirections.Remove(-currentDirection);
            }

            
            if (Random.value < chaseChance)
                currentDirection = GetChaseDirection(validDirections);
            else
                currentDirection = validDirections[Random.Range(0, validDirections.Count)];

            Vector3Int targetCell = wallsTilemap.WorldToCell(transform.position + (Vector3)currentDirection);
            targetPosition = wallsTilemap.GetCellCenterWorld(targetCell);
        }

    }

    Vector2 GetChaseDirection(List<Vector2> validDirections)
    {
        Vector3 playerPos = player.position;
        Vector2 bestDir = validDirections[0];
        float bestDistance = float.MaxValue;

        foreach (Vector2 dir in validDirections)
        {
            Vector3 nextPos = transform.position + (Vector3)dir;
            float dist = Vector3.Distance(nextPos, playerPos);

            if (dist < bestDistance)
            {
                bestDistance = dist;
                bestDir = dir;
            }
        }

        return bestDir;
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

    }
    public void EnableMovement()
    {
        canMove = true;
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

