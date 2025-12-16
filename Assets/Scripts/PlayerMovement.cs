using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;          // Units per second
    private float originalSpeed;
    public Tilemap wallsTilemap;          // Reference to your Walls Tilemap

    private Vector3 targetPosition;       // World position to move toward
    private Vector2 input;                // Current movement direction
    private Vector2 nextInput;            // Buffered input for smooth turning

    private bool canMove = true;

    [SerializeField] private Animator anim;


    public GameEvent StopGhostMovenet;
    [SerializeField] private SpriteRenderer sprite;


    private void Start()
    {
        // Snap player to the center of the current tile
        targetPosition = wallsTilemap.GetCellCenterWorld(wallsTilemap.WorldToCell(transform.position));
        transform.position = targetPosition;
        originalSpeed = moveSpeed;
    }

    private void Update()
    {
        if (canMove)
        {
            HandleInput();
            Move();
        }

    }

    void HandleInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 dir = new Vector2(horizontal, vertical);

        
        if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
        {
            dir.y = 0;
        }

        else
        {
            dir.x = 0;

        }
            

        if (dir != Vector2.zero)
            nextInput = dir;
    }

    void Move()
    {
        if ((transform.position - targetPosition).sqrMagnitude < 0.0001f)
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
                {
                    input = Vector2.zero;
                    anim.SetTrigger("Stop");
                }

            }

            if (input.x != 0)
            {
                // Horizontal movement
                sprite.flipX = input.x < 0;
                transform.rotation = Quaternion.identity; // reset Z rotation
            }
            else if (input.y > 0)
            {
                // Moving up
                sprite.flipX = false;
                transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            }
            else if (input.y < 0)
            {
                // Moving down
                sprite.flipX = false;
                transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            }

            // Set target position to the **center of the next tile**
            if (input != Vector2.zero)
            {
                Vector3Int targetCell = wallsTilemap.WorldToCell(transform.position + (Vector3)input);
                targetPosition = wallsTilemap.GetCellCenterWorld(targetCell);
                anim.SetTrigger("Move");
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

    public void PlayerDeath()
    {
        //Freeze Ghosts
        StopGhostMovenet.Raise();
        //Freeze Players
        canMove = false;

        StartCoroutine(PlayerDeathAnimation());


    }
    public void BecomeTranslucent()
    {
        Debug.Log("translucent");
        sprite.color = new Color(
            1f,
            1f,
            1f,
            0.25f
        );


    }

    public void BecomeUnTranslucent()
    {
        Debug.Log("Non translucent");
        sprite.color = new Color(
            sprite.color.r,
            sprite.color.g,
            sprite.color.b,
            1f
        );
    }
    IEnumerator PlayerDeathAnimation()
    {
        yield return new WaitForSeconds(1f);

        //Play Death Animation
        anim.SetTrigger("Death");
    }

    public void SpeedUpPlayer(int x)
    {
        moveSpeed = x;
    }

    public void ResetSpeedPlayer()
    {
        moveSpeed = originalSpeed;
    }

}
