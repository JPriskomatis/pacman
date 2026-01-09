using System.Collections;
using UnityEngine;

public class ReactPower : PowerUp
{
    [SerializeField] GameObject bomb;
    private int countdown = 3;

    [SerializeField] private GameObject floatingText;
    [SerializeField] private Transform player;


    public override void Ability()
    {
        StartCoroutine(StartCountdown(player));
    }

    protected override IEnumerator DisableAbility()
    {
        throw new System.NotImplementedException();
    }

    private void DropBomb()
    {
        
        Debug.Log("Dropping bomb...");
        bomb.SetActive(true);

    }

    IEnumerator StartCountdown(Transform transform)
    {
        int count = 3;
        
        
        while (countdown > 0)
        {
            GameObject prefab = Instantiate(floatingText, transform.position, Quaternion.identity);
            Vector3 increaseScale = new Vector3(1.5f, 1.5f, 1.5f);
            prefab.transform.localScale += increaseScale;

            prefab.transform.position = new Vector3(player.position.x, player.position.y + 2f, 0);
            prefab.GetComponentInChildren<TextMesh>().text = count.ToString();
            DisplayControllerText.Instance.SetDisplayText(countdown.ToString());
            yield return new WaitForSeconds(1f);
            count--;
            countdown--;
            Destroy(prefab);
        }

        
        Debug.Log("Boom");
        DropBomb();
    }


}
