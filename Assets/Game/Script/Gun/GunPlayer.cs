using UnityEngine;

public class GunPlayer : Gun
{
    private void OnEnable()
    {
        ActionManager.playerShoot += Shoot;
    }
    protected override void Shoot()
    {
        base.Shoot();
    }
}
