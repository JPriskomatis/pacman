using System.Collections;
using UnityEngine;

public class SveltePower : PowerUp
{
    public GameEvent SpeedUpPlayer;
    public GameEvent ResetPlayerSpeed;
    public override void Ability()
    {
        //Makes player go faster;
        SpeedUpPlayer.Raise();

    }

    protected override IEnumerator DisableAbility()
    {
        
        yield return new WaitForSeconds(abilityTimer);
        Debug.Log("Disable ability");
        ResetPlayerSpeed.Raise();
        
    }
}
