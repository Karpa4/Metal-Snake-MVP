using UnityEngine;

namespace Game.Player
{
	public class Carriage : MonoBehaviour, IPlayerHealth
	{
		[SerializeField] private float distance;
		[SerializeField] private SpriteRenderer spriteRenderer;
		[SerializeField] private DustParticle dustParticle;

		private Transform target;
		private Health PlayerHealth;

		public Transform targetForCarriage;

		public void Init(Transform target, Health health, Sprite sprite)
		{
			this.target = target;
			PlayerHealth = health;
			spriteRenderer.sprite = sprite;
		}

		private void Update()
		{
			Vector2 dif = target.position - transform.position;
			transform.up = target.position - transform.position;
			transform.position = target.position - ((Vector3)dif).normalized * distance;
		}

		public void TakeDamage(int damage)
		{
			PlayerHealth.TakeDamage(damage);
		}

        public void TakeCollisionDamage(float damage)
        {
			PlayerHealth.TakeDamage(Mathf.RoundToInt(damage));
		}

        public float GetModifierForEnemy()
        {
			return 1;
        }

		public void SwitchDust(bool isActive)
        {
			if (isActive)
            {
				dustParticle.PauseOff();
            }
			else
            {
				dustParticle.PauseOn();
            }
        }
    }
}
