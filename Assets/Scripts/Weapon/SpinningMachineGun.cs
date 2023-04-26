using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningMachineGun : BaseRotateWeapon
{
    [SerializeField] private float minAngleToStartSpin;
    [SerializeField] private float maxSpinTime;
    private float currentSpinTime = 0;
    private bool isCooling = false;
    private bool hasTarget = false;

    private void Start()
    {
        TargetLost += CollingWeaponWithoutTarget;
        CanShoot = false;
    }

    public override void SetTarget(Transform target)
    {
        hasTarget = true;
        isCooling = false;
        base.SetTarget(target);
    }

    protected override void TargetInRadius(Vector2 aimDirection)
    {
        float angle = Vector2.Angle(transform.up, aimDirection);

        if (angle < minAngleToStartSpin)
        {
            if (currentSpinTime < maxSpinTime)
            {
                if (currentSpinTime <= 0)
                {
                    currentSpinTime = 0;
                    // start play sound
                }

                currentSpinTime += Time.deltaTime;
                if (CanShoot)
                {
                    CanShoot = false;
                }
            }
            else
            {
                if (!CanShoot)
                {
                    CanShoot = true;
                }
            }
        }
        else
        {
            CollingActiveWeapon();
        }
        base.TargetInRadius(aimDirection);
    }

    private void CollingActiveWeapon()
    {
        if (CanShoot)
        {
            CanShoot = false;
        }

        if (currentSpinTime > 0)
        {
            currentSpinTime -= Time.deltaTime;
        }
    }

    protected override void TargetOutRadius()
    {
        CollingActiveWeapon();
        base.TargetOutRadius();
    }

    private void CollingWeaponWithoutTarget()
    {
        hasTarget = false;
        if (currentSpinTime > 0)
        {
            isCooling = true;
            StartCoroutine(StartCooling());
        }
    }

    private IEnumerator StartCooling()
    {
        while (isCooling)
        {
            currentSpinTime -= Time.deltaTime;
            if (currentSpinTime < 0)
            {
                isCooling = false;
                //stop play sound
            }
            yield return null;
        }
    }

    public override void PauseOff()
    {
        //start play sound
        if (!hasTarget)
        {
            CollingWeaponWithoutTarget();
        }
        base.PauseOff();
    }

    public override void PauseOn()
    {
        //stop play sound
        if (isCooling)
        {
            isCooling = false;
        }
        base.PauseOn();
    }
}
