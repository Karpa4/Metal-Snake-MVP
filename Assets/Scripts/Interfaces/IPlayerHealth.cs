public interface IPlayerHealth: IHealth
{
    void TakeCollisionDamage(float damage);
    float GetModifierForEnemy();
}