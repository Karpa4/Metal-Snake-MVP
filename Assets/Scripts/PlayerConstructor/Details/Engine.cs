using UnityEngine;
using UI.Windows.Garage;

namespace Game.PlayerConstructor
{
    [CreateAssetMenu(fileName = "Engine", menuName = "Details/Engine")]
    public class Engine : Detail
    {
        [SerializeField] private float accelerationModifier;
        [SerializeField] private float carriageSlowModifier;

        public float AccelerationModifyer { get => accelerationModifier; }
        public float CarriageSlowModifyer { get => carriageSlowModifier; }

        public override string GetDetailCharacteristics()
        {
            string allDetailChars = string.Empty;
            allDetailChars += AllCharacteristicsText.GetCharacteristicString(accelerationModifier, AllCharacteristicsText.Acceleration);
            allDetailChars += AllCharacteristicsText.GetCharacteristicString(carriageSlowModifier, AllCharacteristicsText.SlowCarriage);
            return allDetailChars;
        }
    }
}
