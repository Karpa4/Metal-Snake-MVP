using UnityEngine;
using UI.Windows.Garage;

namespace Game.PlayerConstructor
{
    [CreateAssetMenu(fileName = "Bumper", menuName = "Details/Bumper")]
    public class Bumper : Detail
    {
        [SerializeField] private PlayerColliderData bumperColliderGO;
        [SerializeField] private float damageFromObstacleModifier;
        [SerializeField] private float collisionDamageToPlayerModifier;
        [SerializeField] private float collisionDamageToEnemyModifier;

        public PlayerColliderData BumperColliderGO { get => bumperColliderGO; }
        public float DamageFromObstacleModifier { get => damageFromObstacleModifier; }
        public float CollisionDamageToPlayerModifier { get => collisionDamageToPlayerModifier; }
        public float CollisionDamageToEnemyModifier { get => collisionDamageToEnemyModifier; }

        public override string GetDetailCharacteristics()
        {
            string allDetailChars = string.Empty;
            allDetailChars += AllCharacteristicsText.GetCharacteristicString(damageFromObstacleModifier, AllCharacteristicsText.DamageToPlayerFromObst);
            allDetailChars += AllCharacteristicsText.GetCharacteristicString(collisionDamageToPlayerModifier, AllCharacteristicsText.DamageToPlayerFromEnem);
            allDetailChars += AllCharacteristicsText.GetCharacteristicString(collisionDamageToEnemyModifier, AllCharacteristicsText.DamageToEnemies);
            return allDetailChars;
        }
    }
}
