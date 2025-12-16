using System.Collections;
using UnityEngine;

public class NodePower : PowerUp
{
    public GameEvent TransulentPlayer;
    public override void Ability()
    {
        TransulentPlayer.Raise();
        StartCoroutine(DisableAbility());
    }

    protected override IEnumerator DisableAbility()
    {
        throw new System.NotImplementedException();
    }
}
