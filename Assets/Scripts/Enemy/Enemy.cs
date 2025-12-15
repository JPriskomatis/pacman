using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private EnemySO[] enemySO;
    private EnemySO currentEnemySO;
    [SerializeField] private SpriteRenderer image;

    public GameEvent EnemyDeath;

    [SerializeField] private float animSpeed = 0.2f;
    private float animTimer;
    private int animIndex;
    private Vector2 lastDirection;


    void Start()
    {
        currentEnemySO = enemySO[Random.Range(0, enemySO.Length)];
        image.sprite = currentEnemySO.right[0];
    }

    public void SetDirection(Vector2 dir, bool isMoving)
    {
        if (dir == Vector2.zero)
            return;

        lastDirection = dir;

        if (!isMoving)
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
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            return dir.x > 0 ? currentEnemySO.right[index] : currentEnemySO.left[index];
        else
            return dir.y > 0 ? currentEnemySO.up[index] : currentEnemySO.down[index];
    }



    public void Death()
    {
        //Flicker effect
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

        EnemyDeath.Raise();
        Debug.Log("Died");
        Destroy(this.gameObject);
    }
}
