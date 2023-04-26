using UnityEngine;

public class TwoGunRotateWeapon : BaseRotateWeapon
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform flashAnimTransform;
    private Vector3 startLocalPos;

    protected override void Awake()
    {
        startLocalPos = PointSpawnProjectiles.localPosition;
        base.Awake();
    }

    protected override void Attack()
    {
        if (PointSpawnProjectiles.localPosition == startLocalPos)
        {
            flashAnimTransform.localPosition += offset;
            PointSpawnProjectiles.localPosition = startLocalPos + offset;
        }
        else
        {
            flashAnimTransform.localPosition -= offset;
            PointSpawnProjectiles.localPosition = startLocalPos;
        }
        base.Attack();
    }
}
