using System.Collections;
using UnityEngine;

namespace Game
{
    public class EnemyWeaponController : MonoBehaviour
    {
        [SerializeField] private float refreshTime;
        [SerializeField] private int maxOverlapTargets;
        [SerializeField] private LayerMask targetMask;
        [SerializeField] private BaseRotateWeapon weapon;
        [SerializeField] private CarDriverAI carDriverAI;
        private Collider2D[] targets;
        private bool isActive = true;
        private float maxRange;
        private float distance;
        private Transform playerTransform;

        private void Awake()
        {
            carDriverAI.InitEnemy += InitWeapon;
            weapon.TargetLost += StartSearchTarget;
            maxRange = weapon.GetMaxRange();
            targets = new Collider2D[maxOverlapTargets];
        }

        private void InitWeapon()
        {
            playerTransform = carDriverAI.GetPlayerTransform();
            StartSearchTarget();
            carDriverAI.InitEnemy -= InitWeapon;
        }

        private void OnDestroy()
        {
            weapon.TargetLost -= StartSearchTarget;
        }

        private void StartSearchTarget()
        {
            isActive = true;
            StartCoroutine(FindTarget());
        }

        public void DeactiveWeapon()
        {
            weapon.SetTarget(null);
        }

        private IEnumerator FindTarget()
        {
            while (isActive)
            {
                distance = Vector2.Distance(transform.position, playerTransform.position);
                if (distance <= maxRange)
                {
                    int targetsCount = Physics2D.OverlapCircleNonAlloc(transform.position, maxRange, targets, targetMask);
                    if (targetsCount > 0)
                    {
                        weapon.SetTarget(targets[Random.Range(0, targetsCount)].transform);
                        isActive = false;
                    }
                }

                yield return new WaitForSeconds(refreshTime);
            }
        }
    }
}
