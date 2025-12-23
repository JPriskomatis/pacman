using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] GameObject player;
    private void Start()
    {
        transform.position = player.transform.position;
        Destroy(gameObject, 1.5f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Found Enemy");
            collision.GetComponent<Enemy>().Death();
        }
    }
}
