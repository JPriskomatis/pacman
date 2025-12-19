using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class PowerUp : MonoBehaviour, Iinteractable
{
    //[SerializeField] private SpriteRenderer image;
    //[SerializeField] private Sprite[] logos;

    //private static int logoIndex;


    public GameEvent PowerUpEvent;

    

    [SerializeField] protected float abilityTimer;

    public void Interact()
    {
        //Destroy(gameObject);
        
        OnPickUp();
        PowerUpEvent.Raise();
        Ability();
        StartCoroutine(DisableAbility());
    }

    //private void Start()
    //{

        
    //    image.sprite = logos[logoIndex];
    //    logoIndex++;
    //    if(logoIndex > logos.Length)
    //    {
    //        logoIndex = 0;
    //    }
    //}

    //private void GetPowerUp()
    //{
    //    image.sprite = logos[logoIndex];
    //    logoIndex++;
    //    if (logoIndex >= logos.Length)
    //    {
    //        logoIndex = 0;
    //    }
    //}

    public abstract void Ability();
    protected abstract IEnumerator DisableAbility();

    protected void OnPickUp()
    {
        this.GetComponent<CircleCollider2D>().enabled = false;
        this.GetComponent<SpriteRenderer>().enabled = false;
    }
}
