using System.Collections;
using UnityEngine;

public class NodePower : PowerUp
{

    public override void Ability()
    {
        

        StartCoroutine(DisableAbility());
    }

    protected override IEnumerator DisableAbility()
    {
        yield return new WaitForSeconds(abilityTimer);

    }
}
