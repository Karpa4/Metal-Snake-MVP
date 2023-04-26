using UnityEngine;

public class PlayerColliderData : MonoBehaviour, IPlayerHealth
{
    private Health health;
    private float damageToPlayerModifier = 1;
    private float damageToEnemyModifier = 1;

    public void ConstructHealth(Health health)
    {
        this.health = health;
    }

    public void ConstructModifiers(float damageToPlayerModifier, float damageToEnemyModifier)
    {
        this.damageToPlayerModifier *= damageToPlayerModifier;
        this.damageToEnemyModifier *= damageToEnemyModifier;
    }

    public float GetModifierForEnemy()
    {
        return damageToEnemyModifier;
    }

    public void TakeCollisionDamage(float damage)
    {
        health.TakeDamage(Mathf.RoundToInt(damage * damageToPlayerModifier));
    }

    public void TakeDamage(int damage)
    {
        health.TakeDamage(damage);
    }
}
