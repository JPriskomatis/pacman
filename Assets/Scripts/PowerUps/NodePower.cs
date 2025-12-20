using System.Collections;
using UnityEngine;

public class NodePower : PowerUp
{
    public GameEvent CanTeleport;
    [SerializeField] private FloatVariable teleportCharges;
    [SerializeField] private float setTeleportCharges = 3f;
    public override void Ability()
    {
        CanTeleport.Raise();
        teleportCharges.value = setTeleportCharges;

    }

    protected override IEnumerator DisableAbility()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);

    }
}
