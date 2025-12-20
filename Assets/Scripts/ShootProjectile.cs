using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    
    public float speed = 10f;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Move straight in the direction the projectile is facing
        rb.linearVelocity = transform.right * speed;

        // Destroy after 3 seconds
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().Death();
            Destroy(gameObject);
        }
    }
}

