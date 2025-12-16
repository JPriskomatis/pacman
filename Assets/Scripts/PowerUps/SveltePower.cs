using System.Collections;
using UnityEngine;

public class SveltePower : PowerUp
{
    public GameEvent SpeedUpPlayer;
    public GameEvent ResetPlayerSpeed;
    public override void Ability()
    {
        OnPickUp();
        //Makes player go faster;
        SpeedUpPlayer.Raise();

        StartCoroutine(DisableAbility());
    }

    protected override IEnumerator DisableAbility()
    {
        
        yield return new WaitForSeconds(abilityTimer);
        Debug.Log("Disable ability");
        ResetPlayerSpeed.Raise();
        Destroy(gameObject);
    }
}
