using System.Collections;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class Enemy : MonoBehaviour, Iinteractable
{

    [SerializeField] private EnemySO[] enemySO;
    private static int enemyIndex;
    private EnemySO currentEnemySO;

    [SerializeField] private SpriteRenderer image;
    [SerializeField] private Sprite originalImage;
    private bool isScaredEnding;

    private bool isDead;
    private Coroutine scaredCoroutine;


    public GameEvent EnemyDeath;

    [SerializeField] private float animSpeed = 0.2f;
    private float animTimer;
    private int animIndex;
    private Vector2 lastDirection;

    private bool isAfraid = false;

    public GameEvent PlayerDies;
    public GameEvent NoLongerScared;

    private bool passThroughPlayer;

    public GameEvent StartEnemySpawnTimer;

    [SerializeField] private Collider2D enemyCollider;
    [SerializeField] private EnemyMovement enemyMovement;

    [SerializeField] private AudioClip clip;
    void Start()
    {
        EnemyLogo();
        image.sprite = currentEnemySO.right[0];
        originalImage = image.sprite;
    }

    private void EnemyLogo()
    {
        currentEnemySO = enemySO[enemyIndex];
        enemyIndex++;
        if(enemyIndex >= enemySO.Length)
        {
            enemyIndex = 0;
        }
    }

    public void SetDirection(Vector2 dir, bool isMoving)
    {
        if (dir == Vector2.zero && !isAfraid)
            return;

        lastDirection = dir;

        
        if (!isMoving && !isAfraid)
        {
            animIndex = 0;
            image.sprite = GetSprite(dir, animIndex);
            return;
        }

        animTimer += Time.deltaTime;

        if (animTimer >= animSpeed)
        {
            animTimer = 0f;
            animIndex = (animIndex + 1) % 2; 
            image.sprite = GetSprite(dir, animIndex);
        }
    }


    Sprite GetSprite(Vector2 dir, int index)
    {
        if (isAfraid)
        {
            Sprite[] sprites = isScaredEnding
                ? currentEnemySO.afraidEnd
                : currentEnemySO.afraid;

            return sprites[index % sprites.Length];
        }

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            return dir.x > 0 ? currentEnemySO.right[index] : currentEnemySO.left[index];
        else
            return dir.y > 0 ? currentEnemySO.up[index] : currentEnemySO.down[index];
    }




    public void Death()
    {
        if (isDead)
            return;

        isDead = true;

        // Stop scared logic immediately
        if (scaredCoroutine != null)
            StopCoroutine(scaredCoroutine);

        isAfraid = false;
        isScaredEnding = false;

        AudioManager.instance.PlayAudioInstance(clip, 0.4f);
        enemyCollider.enabled = false;
        enemyMovement.StopMovement();

        StartCoroutine(FlickerEffect());
    }


    IEnumerator FlickerEffect()
    {
        float elapsed = 0f;
        float flickerInterval = 0.1f; // time between flickers
        Color originalColor = image.color;

        while (elapsed < 1.5f)
        {
            // Toggle alpha between 0 and 1
            float newAlpha = image.color.a > 0.5f ? 0f : 1f;
            image.color = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);

            elapsed += flickerInterval;
            yield return new WaitForSeconds(flickerInterval);
        }

        // Ensure sprite ends at full opacity
        image.color = originalColor;

        StartEnemySpawnTimer.Raise();
        EnemyDeath.Raise();
        Debug.Log("Died");
        Destroy(this.gameObject);
    }

    public void GetScared()
    {
        if (isDead)
            return;

        if (scaredCoroutine != null)
            StopCoroutine(scaredCoroutine);

        scaredCoroutine = StartCoroutine(ScaredGhost());
    }

    IEnumerator ScaredGhost()
    {
        isAfraid = true;
        isScaredEnding = false;
        animIndex = 0;

        float timer = 0f;
        float duration = 5f;
        float flickerStartTime = 3.5f;
        float flickerInterval = 0.2f;
        float flickerTimer = 0f;

        while (timer < duration)
        {
            if (isDead)
                yield break;

            SetDirection(lastDirection, true);

            if (timer >= flickerStartTime)
            {
                flickerTimer += Time.deltaTime;
                if (flickerTimer >= flickerInterval)
                {
                    flickerTimer = 0f;
                    isScaredEnding = !isScaredEnding;
                }
            }

            timer += Time.deltaTime;
            yield return null;
        }

        if (isDead)
            yield break;

        isAfraid = false;
        isScaredEnding = false;
        animIndex = 0;
        image.sprite = GetSprite(lastDirection, animIndex);

        NoLongerScared.Raise();
    }




    public void AllowPassthrough(bool allow)
    {
        passThroughPlayer = allow;
    }

    public void Interact()
    {
        if (!isAfraid && !passThroughPlayer)
        {
            //player dies;
            PlayerDies.Raise();
        }
        else if(isAfraid)
        {
            Death();
        }
        else if (passThroughPlayer)
        {
            Debug.Log("Passing through");
        }
    }
}
