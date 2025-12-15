using UnityEngine;

public class Collidables : MonoBehaviour
{
    public GameEvent PowerUp;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<Iinteractable>().Interact();
    }

}
