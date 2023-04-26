using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class PlayerDeath : MonoBehaviour
    {
        [SerializeField] private PlayerPause playerPause;
        [SerializeField] private Collider2D playerCollider;
        [SerializeField] private List<GameObject> playerPartsTurnOffAfterDeath;
        [SerializeField] private float checkEnemyRadius;
        [SerializeField] private LayerMask targetMask;

        public void TurnOffVisiblePlayerParts()
        {
            playerCollider.enabled = false;
            foreach (GameObject go in playerPartsTurnOffAfterDeath)
            {
                go.SetActive(false);
            }
            playerPause.PauseOn();
            TurnOffEnemyWeapon();
        }

        private void TurnOffEnemyWeapon()
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, checkEnemyRadius, targetMask);
            foreach(Collider2D enemy in enemies)
            {
                if (enemy.TryGetComponent<EnemyWeaponController>(out EnemyWeaponController enemyWeapon))
                {
                    enemyWeapon.DeactiveWeapon();
                }
            }
        }
    }
}
