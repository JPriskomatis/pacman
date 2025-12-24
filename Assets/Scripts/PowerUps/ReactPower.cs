using System.Collections;
using UnityEngine;

public class ReactPower : PowerUp
{
    [SerializeField] GameObject bomb;
    private int countdown = 3;

    public override void Ability()
    {
        StartCoroutine(StartCountdown());
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

    IEnumerator StartCountdown()
    {
        while (countdown > 0)
        {
            DisplayControllerText.Instance.SetDisplayText(countdown.ToString());
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        Debug.Log("Boom");
        DropBomb();
    }
}
