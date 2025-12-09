using UnityEngine;

public class CollectPoints : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Point"))
        {
            Debug.Log("+1");
            Destroy(collision.gameObject);
        }
    }
}
