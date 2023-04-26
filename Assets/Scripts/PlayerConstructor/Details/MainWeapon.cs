using UnityEngine;
using UI.Windows.Garage;

namespace Game.PlayerConstructor
{
    [CreateAssetMenu(fileName = "MainWeapon", menuName = "Details/MainWeapon")]
    public class MainWeapon : Detail
    {
        [SerializeField] private WeaponData weaponData;
        [SerializeField] private Sprite mainStandSprite;
        [SerializeField] private WeaponConstructHelper mainWeaponGO;

        public Sprite MainStandSprite { get => mainStandSprite; }
        public WeaponData WeaponStats { get => weaponData; }
        public WeaponConstructHelper MainWeaponGO { get => mainWeaponGO; }

        public override string GetDetailCharacteristics()
        {
            string allDetailChars = AllCharacteristicsText.WeaponDamage + weaponData.Damage + "\n";
            allDetailChars += AllCharacteristicsText.WeaponRange + weaponData.MaxRange + "\n";
            allDetailChars += AllCharacteristicsText.WeaponReloadTime + weaponData.TimeBeetwenFire;
            return allDetailChars;
        }
    }
}
