using Game;
using UnityEngine;

public class ShotgunRotateWeapon : BaseRotateWeapon
{
    [SerializeField] private float maxSpread;
    [SerializeField] private int bulletQTY;
    [SerializeField] private float maxBulletDistance;

    protected override void Attack()
    {
        for (int i = 0; i < bulletQTY; i++)
        {
            PointSpawnProjectiles.localRotation = Quaternion.identity;
            float spread = Random.Range(-maxSpread, maxSpread);
            PointSpawnProjectiles.Rotate(new Vector3(0, 0, spread));
            base.Attack();
        }
        PointSpawnProjectiles.localRotation = Quaternion.identity;
    }

    protected override void SetProjectileSettings(Projectile p)
    {
        p.SetSpeedAndDistance(GetSpeedProjectile(), maxBulletDistance);
    }
}
