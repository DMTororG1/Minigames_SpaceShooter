using UnityEngine;

[CreateAssetMenu(fileName = "PowerUp_Shield", menuName = "Gameplay/PowerUps/Shield", order = 4)]
public class PowerUp_Shield : PowerUpBase
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private uint bulletAbsortion = 5;

    private GameObject shieldObject;
    private PowerUp_ShieldBehaviour playerShield;

    public PowerUp_Shield() : base()
    {
        type = PowerUpsManager.PowerUpType.Shield;
        useDuration = true;
        durationRange = new Vector2(10f, 10f);
    }

    public override void OnActivate()
    {
        shieldObject = Instantiate(prefab, shipBase.transform);
        playerShield = shieldObject.GetComponent<PowerUp_ShieldBehaviour>();

        playerShield.Init((int)bulletAbsortion);
    }

    public override void OnDeactivate()
    {
        playerShield.Disable().Forget();
        playerShield = null;
    }

    public override void OnEnemyDamage(ShipBaseController enemy)
    {

    }

    public override void OnEnemyDeath(ShipBaseController enemy)
    {

    }

    public override void OnGameUpdate()
    {

    }

    public override void OnPlayerDamage()
    {

    }

    public override void OnPlayerShoot()
    {

    }
}