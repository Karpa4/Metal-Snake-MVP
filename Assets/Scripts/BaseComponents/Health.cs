using System;
using UnityEngine;

[Serializable]
public class Health
{
	[SerializeField] private int health;
	[SerializeField] private int maxHealth;

	public event Action<float> onHealthChanged;
	public event Action onDeath;

	public void Init()
	{
		health = maxHealth;
		HealthChangedInvoke();
	}

	public void Init(int maxHealth)
	{
		this.maxHealth = maxHealth;
		health = maxHealth;
		HealthChangedInvoke();
	}

	public void TakeDamage(int damage)
	{
		if (health > 0)
        {
			health -= damage;
			HealthChangedInvoke();
			if (health <= 0)
            {
				onDeath?.Invoke();
            }
		}
	}

	public bool IsDead()
	{
		return health <= 0;
	}

	public void FullRestore()
	{
		health = maxHealth;
		HealthChangedInvoke();
	}

	private void HealthChangedInvoke()
    {
		float ratio = (float)health / maxHealth;
		onHealthChanged?.Invoke(ratio);
	}
}
