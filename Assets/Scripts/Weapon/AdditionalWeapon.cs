using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Services.Pause;

namespace Game
{
    public class AdditionalWeapon : BaseWeapon, IPause, ISetWeapon
    {
        [SerializeField] private GameObject rocketSprite;
        [SerializeField] private Transform pointSpawnProjectiles;
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private AudioSource source;
        [SerializeField] private ParticleSystem smokeParticle;

        private BaseRotateWeapon playerMainWeapon;
        private WeaponData weaponData;
        private PauseManager pauseManager;

        public int bullets = 0;
        public float timeToReload { get; private set;}

        public event Action<int> onAmmoChanged;

        public bool isReloaded = true;
        public bool doubleTap;

        private void Update() 
        {
            if (doubleTap && bullets > 0)
            {
                AdditionalWeaponAttack(); // Если addattack и сбрасывается цель, то addattack false
            }
            if (!isReloaded)
            {
                timeToReload -= Time.deltaTime;
                if (timeToReload <= 0)
                {
                    Debug.Log("Rocket reloaded");
                    isReloaded = true;
                    if (bullets > 0)
                    {
                        rocketSprite.SetActive(true);
                    }
                }
            }
        }

        public void AdditionalWeaponAttack()
        {
            audioManager.PlaySound(weaponData.WeaponSound, source);
            Debug.Log("Rocket spawned");
            rocketSprite.SetActive(false);
            BaseProjectile p = Instantiate(weaponData.Projectile, pointSpawnProjectiles.position, pointSpawnProjectiles.rotation);
            p.SetSpeed(weaponData.SpeedProjectile);
            p.SetRotationParams(weaponData.RotationSpeed, weaponData.RotationError);
            p.SetTarget(playerMainWeapon.GetTarget());
            p.InitBase(pauseManager, weaponData.Damage);

            bullets--;
            onAmmoChanged?.Invoke(bullets);
            timeToReload = weaponData.TimeBeetwenFire;
            isReloaded = false;
            doubleTap = false;
            smokeParticle.Play();
        }

        public void SetWeaponParams(WeaponData secondWeaponData, int bullets)
        {
            weaponData = secondWeaponData;
            this.bullets = bullets;
            onAmmoChanged?.Invoke(bullets);
        }

        private void OffDoubleTap()
        {
            doubleTap = false;
        }

        public override void SetPauseManager(PauseManager pauseManager)
        {
            this.pauseManager = pauseManager;
            pauseManager.Register(this);
        }

        private void OnDestroy()
        {
            if (pauseManager != null)
            {
                pauseManager.Unregister(this);
            }
        }

        public void PauseOn()
        {
            this.enabled = false;
        }

        public void PauseOff()
        {
            this.enabled = true;
        }

        public float GetTimeBetweenFire()
        {
            return weaponData.TimeBeetwenFire;
        }

        public void SetMainWeapon(BaseRotateWeapon weapon)
        {
            if (playerMainWeapon != null)
            {
                playerMainWeapon.TargetLost -= OffDoubleTap;
            }
            playerMainWeapon = weapon;
            playerMainWeapon.TargetLost += OffDoubleTap;
        }
    }
}
