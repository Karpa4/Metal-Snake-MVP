using UnityEngine;
using Game;
using System;
using Game.Player;
using UnityEngine.Rendering.Universal;

public class MainPlayer : MonoBehaviour, ISetWeapon
{
	[SerializeField] private Health health;
	[SerializeField] private CarDriver carDriver;
	[SerializeField] private Rigidbody2D playerRigidbody;
	[SerializeField] private AdditionalWeapon additionalWeapon;
	[SerializeField] private PlayerDeath playerDeath;
	[SerializeField] private AudioManager audioManager;
	[SerializeField] private AudioSource collisionSource;
	[SerializeField] private AudioSource explosionSource;
	[SerializeField] private GameObject sparksPrefab;

	private Controller controller = new Controller();
	public AudioSource ExplosionSource { get => explosionSource; }
	public Controller PlayerController { get => controller; }

	private BaseRotateWeapon mainWeapon;
	private Vector3 dir;
	private Camera cam;
	private ExplosionController explosion;
	private float collisionBumperModifier = 1;

	private float doubleTapTimer = 0.7f;
	private bool canDoubleTap;
	private GameObject oldTarget;

	public event Action PlayerDeathEvent;
	public Health PlayerHealth => health;

	// Collision System
	private const float speedAfterCollision = 1;
	private const float maxCollisionAngleForTurn = 25;
	private bool isInCollision = false;
	private float collisionTurnAmount = 0;
	private const float collisionPowerForSlow = 1;
	private const float collisionAngleForNewDirection = 30;

	private void Start()
	{
		dir = transform.up;

		//for tests
		health.Init(carDriver.GetMaxHP());

		controller.Init();
		controller.onMove += SetDir;
		controller.onTouchPos += TouchEnemy;

		cam = RefContainer.Instance.MainCamera;

		explosion = FindObjectOfType<ExplosionController>();
	}

	public void SetBumperModifier(float BumperModifier)
	{
		collisionBumperModifier *= BumperModifier;
	}

	private void SlowDown()
	{
		carDriver.SetSpeed(speedAfterCollision);
	}

	public void PlayLightExplosionSound()
	{
		audioManager.PlaySound(AudioManager.Sound.LightExplosion, explosionSource); // Воспроизводим звук уничтожения прицепа
	}

	public void Die()
	{
		audioManager.PlaySound(AudioManager.Sound.HeavyExplosion, collisionSource); //Воспроизводим звук уничтожения игрока
		playerDeath.TurnOffVisiblePlayerParts();
		PlayerDeathEvent?.Invoke();
		explosion.HeavyExplosion(transform.position);		
		DisablePlayerShadow();
	}

	public void TouchEnemy(Vector2 posScreen)
	{
		Ray r = cam.ScreenPointToRay(posScreen);
		RaycastHit2D hit = new RaycastHit2D();
		hit = Physics2D.GetRayIntersection(r, 100);
		if (hit.collider)
		{
			Debug.Log(hit.collider.tag);
			if (hit.collider.CompareTag(ConstantVariables.TAG_ENEMY_TARGET_COLLIDER) || hit.collider.CompareTag(ConstantVariables.TAG_ENEMY))
			{
				controller.SetOffTouch();
				mainWeapon.SetTarget(hit.collider.transform);
				GameObject newTarget = hit.collider.gameObject;

				if (oldTarget == null) // Если цель отсутствует
				{
					MarkAim(newTarget); // Отмечаем новую цель прицелом
				}
				else if (newTarget != oldTarget) // Если новая цель отличается от предыдущей
				{
					MarkNotAim(oldTarget); // Убираем с предыдущей прицел
					MarkAim(newTarget); // Отмечаем новую цель прицелом
				}



				if (canDoubleTap == true && additionalWeapon.isReloaded)  // Если ракета перезаряжена и игрок делает двойной тап
				{
					additionalWeapon.doubleTap = true; // Даем допуск для запуска ракеты 
				}

				canDoubleTap = true;
				Invoke("DoubleTapTimerOver", doubleTapTimer); // По истечении этого времени, следующий тап не будет считаться двойным	
			}
		}
	}

	private void MarkAim(GameObject enemy)
	{
		enemy.GetComponent<AimSpriteController>().EnableAimSprite();
		oldTarget = enemy;
	}

	private void MarkNotAim(GameObject enemy)
	{
		enemy.GetComponent<AimSpriteController>().DisableAimSprite();
	}

	private void DisablePlayerShadow()
	{
		GetComponent<ShadowCaster2D>().enabled = false;
	}


	/// <summary>
	/// Сбрасываем счетчик тапов
	/// </summary>
	private void DoubleTapTimerOver() 
    {
		canDoubleTap = false;
    }

	private void SetDir(Vector2 swipe)
	{
		dir = swipe.normalized;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q))
        {
			Die();
        }

		if (isInCollision)
        {
			carDriver.SetInputs(1, collisionTurnAmount);
			return;
		}

		if (Vector2.Dot((Vector2)transform.up, dir) > 0.95f)
		{
			carDriver.SetInputs(1, 0);
			return;
		}

		float turnAmount;
		float angleToDir = Vector2.SignedAngle((Vector2)transform.up, (Vector2)dir.normalized);
		if (angleToDir > 0)
		{
			turnAmount = 1f;
		}
		else
		{
			turnAmount = -1f;
		}

		carDriver.SetInputs(1, turnAmount);

	}

	private void OnDestroy()
	{
		controller.onMove -= SetDir;
		controller.onTouchPos -= TouchEnemy;
	}

    private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.collider.CompareTag(ConstantVariables.TAG_OBSTACLE))
		{
			// Вычисляем полную силу столкновения
			float collisionPower = 0;
			ContactPoint2D[] contactPoints = new ContactPoint2D[4];
			int contacsCount = other.GetContacts(contactPoints);
			for (int i = 0; i < contacsCount; i++)
            {
				collisionPower += contactPoints[i].normalImpulse;
			}

			// Воспроизводим звук столкновения игрока с препятствием
			if (collisionPower > ConstantVariables.MinCollisionPowerForSound)
			{
				audioManager.PlaySound(AudioManager.Sound.PropsCollision, collisionSource); 
			}

			int damage = 0;
			// вычисляем находится ли точка столкновения в передней части автомобиля
			Vector2 dirToHitPoint = other.GetContact(0).point - (Vector2)transform.position;
			float angle = Vector2.Angle(transform.up, dirToHitPoint);
			
			if (angle <= maxCollisionAngleForTurn)
			{
				Vector2 reflect = Vector2.Reflect(transform.up, other.GetContact(0).normal);
				if (Vector2.SignedAngle(transform.up, reflect) < 0)
				{
					collisionTurnAmount = -1.2f;
				}
				else
				{
					collisionTurnAmount = 1.2f;
				}

				if (collisionPower > collisionPowerForSlow)
				{
					isInCollision = true;
					SlowDown();
				}
				else if (collisionPower == 0)
                {
					isInCollision = true;
				}
				damage = Mathf.RoundToInt(collisionPower * ConstantVariables.CollisionHealthModifier * collisionBumperModifier);
			}
			else
            {
				damage = Mathf.RoundToInt(collisionPower * ConstantVariables.CollisionHealthModifier);
			}
			health.TakeDamage(damage);

			if (other.GetContact(0).point != null)
			{
				var sparks = Instantiate(sparksPrefab, other.GetContact(0).point, Quaternion.identity);
				Destroy(sparks, 1);
			}
		}
	}

    private void OnCollisionStay2D(Collision2D collision)
    {
		if (collision.collider.CompareTag(ConstantVariables.TAG_OBSTACLE))
		{
			Vector2 normal = collision.GetContact(0).normal;
			if (Vector2.Angle(normal, dir) < 88)
            {
				isInCollision = false;
				return;
			}

			Vector2 transUp = transform.up;
			Vector2 reflect = Vector2.Reflect(transUp, normal);
			float angle = Vector2.Angle(transUp, reflect);
			SetDir(transUp);

			if (angle < collisionAngleForNewDirection)
			{
				Vector2 newDirection = transUp.normalized - Vector2.Dot(transUp, normal) * normal;
				SetDir(newDirection);
				collisionTurnAmount = 0;
				isInCollision = false;
			}
		}
	}

    private void OnCollisionExit2D(Collision2D collision)
    {
		if (collision.collider.CompareTag(ConstantVariables.TAG_OBSTACLE))
		{
			isInCollision = false;
			collisionTurnAmount = 0;
		}
	}

	public void PauseOn()
    {
		controller.DisableControll();
	}

	public void PauseOff()
	{
		controller.EnableControll();
	}

    public void SetMainWeapon(BaseRotateWeapon weapon)
    {
		mainWeapon = weapon;
		mainWeapon.Init(audioManager, explosionSource, transform);
	}
}
