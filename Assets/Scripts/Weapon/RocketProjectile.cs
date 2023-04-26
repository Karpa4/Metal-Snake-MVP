using Services.Pause;
using UnityEngine;

namespace Game
{
	public class RocketProjectile : BaseProjectile
	{
		[SerializeField] private float rotationModifier;
		[SerializeField] private float minAngleToModRotation;
		[SerializeField] private LayerMask enemyMask;
		[SerializeField] private float explosionRadius;
		[SerializeField] private int explosionDamage;
		[SerializeField] private int maxTargets;
		private float rotationSpeed = 0;
		private float rotationError = 0;
		private float speed = 0;
		private Transform target;

		private void Update()
		{
			if (target != null)
			{
				Vector2 targetDir = target.position - transform.position;
				float angle = Vector2.Angle(transform.up, targetDir);
				if (angle > rotationError)
				{
					float modifier;
					Quaternion newRotation = Quaternion.LookRotation(Vector3.forward, targetDir);
					if (angle > minAngleToModRotation)
                    {
						modifier = rotationModifier;
					}
					else
                    {
						modifier = 1;
					}
					transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, Time.deltaTime * rotationSpeed * modifier);
				}
			}
			transform.position = transform.position + transform.up * speed * Time.deltaTime;
		}

        public override void InitBase(PauseManager pauseManager, int damage)
        {
			damage -= explosionDamage;
            base.InitBase(pauseManager, damage);
        }

        public override void SetSpeed(float speed)
		{
			BeforeDestroy += HitEnemiesInRadius;
			this.speed = speed;
		}

		public override void SetTarget(Transform target)
        {
			this.target = target;
			this.enabled = true;
		}

        public override void SetRotationParams(float rotationSpeed, float rotationError)
        {
			this.rotationSpeed = rotationSpeed;
			this.rotationError = rotationError;
		}

		public override void PauseOn()
		{
			this.enabled = false;
		}

		public override void PauseOff()
		{
			this.enabled = true;
		}

		private void HitEnemiesInRadius()
        {
			BeforeDestroy -= HitEnemiesInRadius;
			Collider2D[] enemies = new Collider2D[maxTargets];
			int qty = Physics2D.OverlapCircleNonAlloc(transform.position, explosionRadius, enemies, enemyMask);
			if (qty > 0)
            {
                for (int i = 0; i < qty; i++)
                {
					if (enemies[i].TryGetComponent<IHealth>(out IHealth health))
                    {
						health.TakeDamage(explosionDamage);
					}
				}
            }
        }
    }
}
