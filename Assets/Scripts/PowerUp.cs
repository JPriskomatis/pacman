using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour, Iinteractable
{
    [SerializeField] private SpriteRenderer image;
    [SerializeField] private Sprite[] logos;

    public GameEvent PowerUpEvent;

    public void Interact()
    {
        Destroy(gameObject);
        PowerUpEvent.Raise();

    }

    private void Start()
    {
        int randomLogo = Random.Range(0, logos.Length);
        image.sprite = logos[randomLogo];
    }
}
