
using System.Collections;
using UnityEngine;

public class AstroPower : PowerUp
{
    public GameEvent EnableShootProjectile;
    [SerializeField] private FloatVariable numberOfBullets;
    [SerializeField] private float addBullets = 3f;

    [SerializeField] private StringVariable powerUpText;


    public override void Ability()
    {
        numberOfBullets.value = addBullets;
        EnableShootProjectile.Raise();
        DisplayControllerText.Instance.SetDisplayText(powerUpText.value);
    }

    protected override IEnumerator DisableAbility()
    {
        yield return new WaitForSeconds(0f);
        Destroy(gameObject);
    }
}
