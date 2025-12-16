using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour, Iinteractable
{
    [SerializeField] private SpriteRenderer image;
    [SerializeField] private Sprite[] logos;

    private static int logoIndex;


    public GameEvent PowerUpEvent;

    public void Interact()
    {
        Destroy(gameObject);
        PowerUpEvent.Raise();
    }

    private void Start()
    {

        
        image.sprite = logos[logoIndex];
        logoIndex++;
        if(logoIndex > logos.Length)
        {
            logoIndex = 0;
        }
    }

    private void GetPowerUp()
    {
        image.sprite = logos[logoIndex];
        logoIndex++;
        if (logoIndex >= logos.Length)
        {
            logoIndex = 0;
        }
    }
}
