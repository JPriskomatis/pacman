using System.Collections;
using UnityEngine;

public class ReactPower : PowerUp
{
    [SerializeField] GameObject bomb;
    public override void Ability()
    {
        StartCoroutine(DropBomb());
    }

    protected override IEnumerator DisableAbility()
    {
        throw new System.NotImplementedException();
    }

    IEnumerator DropBomb()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("Dropping bomb...");
        bomb.SetActive(true);

    }
}
