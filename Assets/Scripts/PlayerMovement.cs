using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using static Unity.VisualScripting.Member;

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
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private SpriteRenderer runningShoes;

    Vector3 pos;

    public GameObject projectilePrefab;
    public Transform firePoint;

    private Vector2 shootDirection = Vector2.right; // Stored shooting direction

    public GameEvent StopGhostMovenet;

    private bool canShoot = false;

    [SerializeField] private FloatVariable numberOfBullets;
    public GameEvent shootingBullet;
    public GameEvent DisableShootingBullet;

    [SerializeField] private int numberOfTeleportDistance = 4;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip teleport, error;

    [SerializeField] private bool canTeleport;
    [SerializeField] private FloatVariable teleportCharges;
    public GameEvent CannotTeleport;
    public GameEvent Teleporting;

    [SerializeField] private GameObject teleportVFX;

    [SerializeField] private AudioClip clip;
    private void Start()
    {
        pos = runningShoes.gameObject.transform.localPosition;
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

            if (Input.GetKeyDown(KeyCode.Space) && canTeleport)
            {
                TryTeleportForward(numberOfTeleportDistance);
            }
        }

        if (canShoot && Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }
    
    public void EnableTeleport(bool allow)
    {
        canTeleport = allow;
    }

    bool IsCellBlocked(Vector3Int cell)
    {
        // Outside tilemap = blocked
        if (!wallsTilemap.cellBounds.Contains(cell))
            return true;

        // Wall tile = blocked
        return wallsTilemap.HasTile(cell);
    }

    void TryTeleportForward(int cells)
    {
        Vector3Int currentCell = wallsTilemap.WorldToCell(transform.position);

        // Use current movement direction OR last facing direction
        Vector2 dir = input != Vector2.zero ? input : shootDirection;

        Vector3Int direction = new Vector3Int(
            Mathf.RoundToInt(dir.x),
            Mathf.RoundToInt(dir.y),
            0
        );

        Vector3Int destinationCell = currentCell + direction * cells;

        if (IsCellBlocked(destinationCell))
        {
            source.clip = error;
            source.Play();
            return;
        }

        source.clip = teleport;
        source.Play();
        TeleportToCell(destinationCell);
    }



    void HandleInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 dir = new Vector2(horizontal, vertical);

        // Prioritize horizontal movement
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
                nextCell = wallsTilemap.WorldToCell(transform.position + (Vector3)input);
                if (IsWall(nextCell))
                {
                    input = Vector2.zero;
                    anim.SetTrigger("Stop");
                }
            }

            if (input.x != 0)
            {
                // Flip sprites horizontally
                bool facingLeft = input.x < 0;
                sprite.flipX = facingLeft;
                runningShoes.flipX = facingLeft;

                // Adjust running shoes position
                float xOffset = 0.324f;
                pos.x = facingLeft ? xOffset : -xOffset;
                runningShoes.transform.localPosition = pos;

                transform.rotation = Quaternion.identity;
            }
            else if (input.y > 0)
            {
                pos.x = -0.235f;
                runningShoes.transform.localPosition = pos;
                sprite.flipX = false;
                runningShoes.flipX = false;
                transform.rotation = Quaternion.Euler(0f, 0f, 90f); // Up
            }
            else if (input.y < 0)
            {
                sprite.flipX = false;
                runningShoes.flipX = false;
                transform.rotation = Quaternion.Euler(0f, 0f, -90f); // Down
            }

            // Update shooting direction
            if (input != Vector2.zero)
            {
                shootDirection = input.normalized;

                // Set target position to the center of the next tile
                Vector3Int targetCell = wallsTilemap.WorldToCell(transform.position + (Vector3)input);
                targetPosition = wallsTilemap.GetCellCenterWorld(targetCell);
                anim.SetTrigger("Move");
            }
        }

        // Smoothly move toward target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    public void EnableShoot(bool allow)
    {
        canShoot = allow;
    }
    void Shoot()
    {
        if(numberOfBullets.value > 0)
        {
            if (projectilePrefab == null || firePoint == null) return;

            // Instantiate projectile
            GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

            // Get Rigidbody2D
            Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();

            // Move in the stored shoot direction
            rb.linearVelocity = shootDirection.normalized * 10f;

            // Rotate projectile to face the direction it is moving
            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
            proj.transform.rotation = Quaternion.Euler(0f, 0f, angle);

            Destroy(proj, 3f);
            
            numberOfBullets.value--;
            shootingBullet.Raise();
            if(numberOfBullets.value == 0)
            {
                DisableShootingBullet.Raise();
                EnableShoot(false);
            }
        }
       
        
    }


    IEnumerator TeleportAnimation()
    {
        teleportVFX.SetActive(true);
        teleportVFX.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        teleportVFX.SetActive(false);
    }
    public void TeleportToCell(Vector3Int cell)
    {
        StartCoroutine(TeleportAnimation());
        

        teleportCharges.value--;
        targetPosition = wallsTilemap.GetCellCenterWorld(cell);
        transform.position = targetPosition;
        Teleporting.Raise();

        if (teleportCharges.value == 0)
        {
            CannotTeleport.Raise();
        }
    }

    bool IsWall(Vector3Int cell)
    {
        return wallsTilemap.HasTile(cell);
    }

    public void PlayerDeath()
    {
        AudioManager.instance.PlayAudioInstance(clip);
        StopGhostMovenet.Raise();
        canMove = false;
        StartCoroutine(PlayerDeathAnimation());
    }

    IEnumerator PlayerDeathAnimation()
    {
        yield return new WaitForSeconds(1f);
        anim.SetTrigger("Death");
        yield return new WaitForSeconds(3f);
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
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
