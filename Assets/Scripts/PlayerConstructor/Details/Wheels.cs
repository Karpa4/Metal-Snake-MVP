using UnityEngine;
using UI.Windows.Garage;

namespace Game.PlayerConstructor
{
    [CreateAssetMenu(fileName = "Wheels", menuName = "Details/Wheels")]
    public class Wheels : Detail
    {
        [SerializeField] private float turnSpeedModifier;
        [SerializeField] private float collisionDamageToPlayerModifier;
        [SerializeField] private float collisionDamageToEnemyModifier;

        public float TurnSpeedModifier { get => turnSpeedModifier; }
        public float CollisionDamageToPlayerModifier { get => collisionDamageToPlayerModifier; }
        public float CollisionDamageToEnemyModifier { get => collisionDamageToEnemyModifier; }

        public override string GetDetailCharacteristics()
        {
            string allDetailChars = string.Empty;
            allDetailChars += AllCharacteristicsText.GetCharacteristicString(turnSpeedModifier, AllCharacteristicsText.TurnSpeedAcceleration);
            allDetailChars += AllCharacteristicsText.GetCharacteristicString(collisionDamageToPlayerModifier, AllCharacteristicsText.DamageToPlayerFromEnem);
            allDetailChars += AllCharacteristicsText.GetCharacteristicString(collisionDamageToEnemyModifier, AllCharacteristicsText.DamageToEnemies);
            return allDetailChars;
        }
    }
}
