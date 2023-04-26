using UnityEngine;
using Game;

[CreateAssetMenu(menuName = "GameData/WeaponData")]
public class WeaponData : ScriptableObject
{
	[SerializeField] private int damage;
	[SerializeField] private float timeBeetwenFire;
	[SerializeField] private float maxRange;
	[SerializeField] private float rotationSpeed;
	[SerializeField] private float rotationError;
	[SerializeField] private BaseProjectile projectile;
	[SerializeField] private float speedProjectile;
	[SerializeField] private AudioManager.Sound weaponSound;

	public int Damage { get => damage; }
	public float TimeBeetwenFire { get => timeBeetwenFire; }
	public BaseProjectile Projectile { get => projectile; }
	public float SpeedProjectile { get => speedProjectile; }
	public float RotationSpeed { get => rotationSpeed; }
	public float MaxRange { get => maxRange; }
	public float RotationError { get => rotationError; }
	public AudioManager.Sound WeaponSound{ get => weaponSound; }
}