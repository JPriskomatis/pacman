using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class BasketCase : MonoBehaviour
{
    private CircleCollider2D circleCollider;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float stunDuration = 5f;

    [SerializeField] private GameObject floatingText;

    [SerializeField] private string[] buyingTexts;

    private bool triggered = false;
    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered) return;
        if (!collision.CompareTag("Enemy")) return;

        triggered = true;

        circleCollider.enabled = false;
        spriteRenderer.enabled = false;

        if (collision.TryGetComponent(out EnemyMovement enemyMovement))
        {
            StartCoroutine(StopEnemyMovement(enemyMovement));
        }

        if (collision.TryGetComponent(out Enemy enemy))
        {
            enemy.GetScared();
        }
    }


    private IEnumerator StopEnemyMovement(EnemyMovement enemy)
    {
        enemy.StopMovement();
        ShowFloatingText(enemy.transform);
        yield return new WaitForSeconds(stunDuration);

        if (enemy != null)
        {
            enemy.EnableMovement();
        }
    }

    void ShowFloatingText(Transform transform)
    {
        if (floatingText)
        {
            GameObject prefab = Instantiate(floatingText, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = buyingTexts[Random.Range(0, buyingTexts.Length)];
            Destroy(prefab, 2f);

        }
    }
}
