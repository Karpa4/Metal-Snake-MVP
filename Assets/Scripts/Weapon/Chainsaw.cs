using UnityEngine;
using System.Collections;
using Services.Pause;

namespace Game
{
    public class Chainsaw : BaseWeapon, IHealth, IPause
    {
        [SerializeField] private WeaponData weaponData;
        [SerializeField] private MainEnemy mainEnemy;
        [SerializeField] private Collider2D weaponCollider;
        [SerializeField] private AudioSource porcupineSource;
        private IHealth health = null;
        private PauseManager pauseManager;
        private AudioManager audioManagerScript;
        private bool chainSawAttacking;
        private bool soundCoroutineActive;

        private void Awake()
        {
            audioManagerScript = FindObjectOfType<AudioManager>();
        }

        private void OnDestroy()
        {
            if (pauseManager != null)
            {
                pauseManager.Unregister(this);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(ConstantVariables.TAG_CARRIAGE))
            {
                health = collision.gameObject.GetComponent<IHealth>();
                StartCoroutine(DealDamage());
                chainSawAttacking = true;

                if (!soundCoroutineActive)
                {
                    StartCoroutine(ChainSawSound()); // если она еще не запущена                   
                }
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(ConstantVariables.TAG_CARRIAGE))
            {
                StopCoroutine(DealDamage());
                health = null;
                chainSawAttacking = false;
            }
        }

        private IEnumerator DealDamage()
        {
            while (health != null)
            {
                health.TakeDamage(weaponData.Damage);
                yield return new WaitForSeconds(weaponData.TimeBeetwenFire);
            }
        }

        private IEnumerator ChainSawSound() // Звук пилы проигрывается не менее 0.75 сек
        {
            soundCoroutineActive = true;
            audioManagerScript.PlaySound(AudioManager.Sound.PorcupineSawAttack, porcupineSource);
            while (true)
            {
                yield return new WaitForSeconds(0.75f);
                if (!chainSawAttacking)
                {
                    porcupineSource.Stop();
                    soundCoroutineActive = false;
                    StopCoroutine(ChainSawSound());
                }
            }
        }

        public void TakeDamage(int damage)
        {
            mainEnemy.TakeDamage(damage);
        }

        public void PauseOn()
        {
            weaponCollider.enabled = false;
        }

        public void PauseOff()
        {
            weaponCollider.enabled = true;
        }

        public override void SetPauseManager(PauseManager pauseManager)
        {
            this.pauseManager = pauseManager;
            pauseManager.Register(this);
        }
    }
}
