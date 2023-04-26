public interface ISetWeapon
{
    void SetMainWeapon(BaseRotateWeapon weapon);
}

public enum WeaponState
{
    RotateToTarget = 0,
    CanShoot,
    TargetLost,
    Off
}
