using System.Collections;
using UnityEngine;

public class NodePower : PowerUp
{
    public GameEvent CanTeleport;
    [SerializeField] private FloatVariable teleportCharges;
    [SerializeField] private float setTeleportCharges = 3f;

    [SerializeField] private StringVariable powerUpText;
    public override void Ability()
    {
        CanTeleport.Raise();
        teleportCharges.value = setTeleportCharges;
        DisplayControllerText.Instance.SetDisplayText(powerUpText.value);

    }

    protected override IEnumerator DisableAbility()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);

    }
}
