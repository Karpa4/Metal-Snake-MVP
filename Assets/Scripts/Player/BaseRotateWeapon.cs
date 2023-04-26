using UnityEngine;
using Services.Pause;
using Game;
using System;

public class BaseRotateWeapon : BaseWeapon, IPause
{
	[SerializeField] private Transform pointSpawnProjectiles;
	[SerializeField] private WeaponData weaponData;
	[SerializeField] private Transform mainTransform;
	[SerializeField] private Transform rotateTransform;
	[SerializeField] private AudioManager audioManager;
	[SerializeField] private AudioSource source;
	[SerializeField] private Animator anim;
	[SerializeField] private BulletPool bulletPool;

	private Transform target;
	private PauseManager pauseManager;
	private bool canShoot = true;
	private bool isActive = false;
	private bool targetLostWasInwoke = false;
	private float timer = 0;

	public Transform PointSpawnProjectiles { get => pointSpawnProjectiles; set => pointSpawnProjectiles = value; }
	public bool CanShoot { get => canShoot; set => canShoot = value; }
	public event Action TargetLost;

    protected virtual void Awake()
    {
        if (gameObject.CompareTag(ConstantVariables.TAG_ENEMY))
        {
			bulletPool = FindObjectOfType<EnemyBulletPool>();
			audioManager = FindObjectOfType<AudioManager>();
		}
    }

    /// <summary>
    /// Внедрение недостающих компонентов для работы скрипта. Вызывается при смене детали или при старте игры.
    /// Используется только игроком
    /// </summary>
    /// <param name="audioManager">AudioManager</param>
    /// <param name="source">AudioSource</param>
    /// <param name="mainPlayerTransform">Трансформ, куда будет вращаться оружие в состоянии покоя</param>
    public void Init(AudioManager audioManager, AudioSource source, Transform mainPlayerTransform)
    {
		this.audioManager = audioManager;
		this.source = source;
		this.mainTransform = mainPlayerTransform;
		rotateTransform = transform.parent;
	}

	public override void SetPauseManager(PauseManager pauseManager)
	{
		this.pauseManager = pauseManager;
		pauseManager.Register(this);
	}

	/// <summary>
	/// Задание новой цели для оружия
	/// </summary>
	/// <param name="target">Новая цель</param>
	public virtual void SetTarget(Transform target)
	{
		this.target = target;
		targetLostWasInwoke = false;
		if (!isActive)
		{
			isActive = true;
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
        Attack();

		if (isActive)
		{
			if (target == null)
			{
                if (!targetLostWasInwoke)
                {
					TargetLost?.Invoke();
					targetLostWasInwoke = true;
				}

				if (IsNeedToRotate(mainTransform.up))
				{
					RotateToTarget(mainTransform.up);
				}
				else
				{
					if (timer <= 0)
					{
						isActive = false;
					}
				}
			}
			else
			{
				Vector2 aimDirection = target.position - rotateTransform.position;
				if (aimDirection.magnitude < weaponData.MaxRange)
				{
					TargetInRadius(aimDirection);
				}
				else
				{
					TargetOutRadius();
				}
			}

			if (timer <= weaponData.TimeBeetwenFire && timer > 0)
			{
				timer -= Time.deltaTime;
			}
		}
	}

	/// <summary>
	/// Нужно ли вращать оружие
	/// </summary>
	/// <param name="secondValue">Вектор до цели</param>
	/// <returns>true - нужно , false - не нужно</returns>
	private bool IsNeedToRotate(Vector2 secondValue)
	{
		float angle = Vector2.Angle(rotateTransform.up, secondValue);
		return angle > weaponData.RotationError;
	}

	/// <summary>
	/// Цель находится за пределами радиуса атаки
	/// </summary>
	protected virtual void TargetOutRadius()
    {
		if (IsNeedToRotate(mainTransform.up))
		{
			RotateToTarget(mainTransform.up);
		}
	}

	/// <summary>
	/// Цель находится в пределах радиуса атаки
	/// </summary>
	/// <param name="aimDirection">Вектор до цели</param>
	protected virtual void TargetInRadius(Vector2 aimDirection)
    {
		if (IsNeedToRotate(aimDirection))
		{
			RotateToTarget(aimDirection);
		}
		else
		{
			if (timer <= 0 && canShoot)
			{
				Attack();
				timer = weaponData.TimeBeetwenFire;
			}
		}
	}

	protected virtual void Attack()
	{
		Projectile p = bulletPool.Bullets.Get();
		p.gameObject.transform.position = pointSpawnProjectiles.position;
		p.gameObject.transform.rotation = pointSpawnProjectiles.rotation;
		p.InitBase(pauseManager, weaponData.Damage);
		SetProjectileSettings(p);
		PlayShotSound();
		anim.Play("Shooting", 0, 0.0f);
	}

	protected virtual void SetProjectileSettings(Projectile p)
    {
		p.SetSpeed(weaponData.SpeedProjectile);
	}

	/// <summary>
	/// Вращение оружия
	/// </summary>
	/// <param name="aimDirection">Направление врашения</param>
	private void RotateToTarget(Vector2 aimDirection)
	{
		Quaternion newRotation = Quaternion.LookRotation(Vector3.forward, aimDirection);
		rotateTransform.rotation = Quaternion.RotateTowards(rotateTransform.rotation, newRotation, Time.deltaTime * weaponData.RotationSpeed);
	}

	private void OnDestroy()
	{
		if (pauseManager != null)
		{
			pauseManager.Unregister(this);
		}
	}

	public Transform GetTarget()
	{
		return target;
	}

	public float GetSpeedProjectile()
	{
		return weaponData.SpeedProjectile;
	}

	public float GetMaxRange()
	{
		return weaponData.MaxRange;
	}

	public virtual void PauseOn()
	{
		this.enabled = false;
	}

	public virtual void PauseOff()
	{
		this.enabled = true;
	}

	private void PlayShotSound()
	{
		audioManager.PlaySound(weaponData.WeaponSound, source);
	}
}
