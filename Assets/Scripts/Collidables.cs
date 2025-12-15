using UnityEngine;

public class Collidables : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Point"))
        {
            CollectPoint(collision);

        } 
        else if (collision.CompareTag("PowerUp"))
        {
            CollectPowerUp(collision);
        }
        else if (collision.CompareTag("Enemy"))
        {
            DestroyEnemy(collision);
        }
    }

    private void DestroyEnemy(Collider2D enemy)
    {
        enemy.GetComponent<Enemy>().Death();
    }

    private void CollectPowerUp(Collider2D powerUp)
    {
        Destroy(powerUp.gameObject);
    }

    private void CollectPoint(Collider2D point)
    {
        Debug.Log("+1");
        Destroy(point.gameObject);
    }
}
