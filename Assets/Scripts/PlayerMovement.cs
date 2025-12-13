using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;          // Units per second
    public Tilemap wallsTilemap;          // Reference to your Walls Tilemap

    private Vector3 targetPosition;       // World position to move toward
    private Vector2 input;                // Current movement direction
    private Vector2 nextInput;            // Buffered input for smooth turning

    private void Start()
    {
        // Snap player to the center of the current tile
        targetPosition = wallsTilemap.GetCellCenterWorld(wallsTilemap.WorldToCell(transform.position));
        transform.position = targetPosition;
    }

    private void Update()
    {
        HandleInput();
        Move();
    }

    void HandleInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 dir = new Vector2(horizontal, vertical);

        // Allow only one direction at a time
        if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
            dir.y = 0;
        else
            dir.x = 0;

        if (dir != Vector2.zero)
            nextInput = dir;
    }

    void Move()
    {
        if ((Vector3)transform.position == targetPosition)
        {
            // Try buffered input first
            Vector3Int nextCell = wallsTilemap.WorldToCell(transform.position + (Vector3)nextInput);
            if (!IsWall(nextCell))
            {
                input = nextInput;
            }
            else
            {
                // If blocked, try current input
                nextCell = wallsTilemap.WorldToCell(transform.position + (Vector3)input);
                if (IsWall(nextCell))
                    input = Vector2.zero;
            }

            // Set target position to the **center of the next tile**
            if (input != Vector2.zero)
            {
                Vector3Int targetCell = wallsTilemap.WorldToCell(transform.position + (Vector3)input);
                targetPosition = wallsTilemap.GetCellCenterWorld(targetCell);
            }
        }

        // Smoothly move toward target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    public void TeleportToCell(Vector3Int cell)
    {
        targetPosition = wallsTilemap.GetCellCenterWorld(cell);
        transform.position = targetPosition;
    }

    bool IsWall(Vector3Int cell)
    {
        return wallsTilemap.HasTile(cell);
    }
}
