using System.Collections;
using UnityEngine;

public class NodePower : PowerUp
{
    public GameEvent TransulentPlayer;
    public GameEvent UnTranslucentPlayer;
    public override void Ability()
    {
        OnPickUp();
        TransulentPlayer.Raise();
        StartCoroutine(DisableAbility());
    }

    protected override IEnumerator DisableAbility()
    {
        yield return new WaitForSeconds(abilityTimer);
        UnTranslucentPlayer.Raise();
    }
}
