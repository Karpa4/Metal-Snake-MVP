using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MainEnemy : MonoBehaviour, IHealth
{
	[SerializeField] private Health health;
	[SerializeField] private CarDriver carDriver;
	[SerializeField] private AudioClip bearClip;
	[SerializeField] private GameObject sparksPrefab;
	private AudioManager audioManager;
	private AudioSource collisionSource;
	private AudioSource explosionSource;
	private ExplosionController explosion;
	private bool canCollideWithPlayer = true;

	private void Start()
	{
		int maxHP = carDriver.GetMaxHP();
		health.Init(maxHP);
		health.onDeath += Die;
		audioManager = FindObjectOfType<AudioManager>();
		collisionSource = GetComponent<AudioSource>();
		explosionSource = FindObjectOfType<MainPlayer>().ExplosionSource;
		explosion = FindObjectOfType<ExplosionController>();
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		float modifier = 1;
		ContactPoint2D contactPoint = other.GetContact(0);
		float collisionPower = contactPoint.normalImpulse;
		float damage = collisionPower * ConstantVariables.CollisionHealthModifier;

		if (other.gameObject.TryGetComponent<IPlayerHealth>(out IPlayerHealth otherHealth))
		{
			if (canCollideWithPlayer)
			{
				otherHealth.TakeCollisionDamage(damage);
				modifier = otherHealth.GetModifierForEnemy();
				health.TakeDamage(Mathf.RoundToInt(damage * modifier));

				CreateSparks(contactPoint.point);
				StartCoroutine(SetOffPlayerCollision());

				//if (collisionPower > ConstantVariables.MinCollisionPowerForSound)
				//{
				//	audioManager.PlaySound(AudioManager.Sound.CarsCollision, collisionSource);
				//}
			}
		}
		else
		{
			health.TakeDamage(Mathf.RoundToInt(damage));

			if (other.gameObject.CompareTag("Obstacle"))
			{
				audioManager.PlaySound(AudioManager.Sound.PropsCollision, collisionSource);
			}

			CreateSparks(contactPoint.point);
		}
	}

	private void CreateSparks(Vector2 point)
    {
		var sparks = Instantiate(sparksPrefab, point, Quaternion.identity);
		Destroy(sparks, 1f);
	}

	private IEnumerator SetOffPlayerCollision()
    {
		canCollideWithPlayer = false;
		yield return new WaitForSeconds(ConstantVariables.DelayBeetwenRepeatCollision);
		canCollideWithPlayer = true;
	}

	public void TakeDamage(int damage)
	{
		health.TakeDamage(damage);
	}

	private void Die()
	{
		if (collisionSource.clip == bearClip) // ≈сли уничтоженный враг - медведь, воспроизводим звук heavy explosion
			audioManager.PlaySound(AudioManager.Sound.HeavyExplosion, explosionSource);

		else
		{
		   audioManager.PlaySound(AudioManager.Sound.LightExplosion, explosionSource); // ѕри уничтожении другого врага, воспроизводитс€ light explosion
		}

		explosion.HeavyExplosion(transform.position);
		Destroy(gameObject);
	}
}
