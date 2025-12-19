using System.Collections;
using UnityEngine;

public class ShopifyPower : PowerUp
{
    [SerializeField] private GameObject basketCase;
    int spawnNumber = 0;

    Transform playerPos;

    private void Start()
    {
        playerPos = FindFirstObjectByType<PlayerMovement>().gameObject.transform;
    }
    public override void Ability()
    {
        //After 3 seconds drop a basket case, twice
        StartCoroutine(DropBasketCase());
    }

    IEnumerator DropBasketCase()
    {
        while(spawnNumber < 2)
        {
            Debug.Log("Spawning basketcase...");
            yield return new WaitForSeconds(3f);
            
            Debug.Log("Players position: " + playerPos);

            GameObject basket = Instantiate(basketCase);
            basket.transform.SetPositionAndRotation(playerPos.position, Quaternion.identity);
 
            basketCase.SetActive(true);
            spawnNumber++;
        }
        
    }

    protected override IEnumerator DisableAbility()
    {
        yield return new WaitForSeconds(6.5f);
        Debug.Log("");
    }

}
