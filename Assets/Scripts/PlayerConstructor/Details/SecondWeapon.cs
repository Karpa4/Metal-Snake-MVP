using UnityEngine;
using UI.Windows.Garage;

namespace Game.PlayerConstructor
{
    [CreateAssetMenu(fileName = "SecondWeapon", menuName = "Details/SecondWeapon")]
    public class SecondWeapon : Detail
    {
        [SerializeField] private WeaponData weaponData;
        [SerializeField] private Sprite secondStandSprite;
        [SerializeField] private Sprite secondStandUISprite;
        [SerializeField] private int bullets;

        public Sprite SecondStandSprite { get => secondStandSprite; }
        public Sprite SecondStandUISprite { get => secondStandUISprite; }
        public WeaponData WeaponStats { get => weaponData; }
        public int Bullets { get => bullets; }

        public override string GetDetailCharacteristics()
        {
            string allDetailChars = AllCharacteristicsText.WeaponDamage + weaponData.Damage + "\n";
            allDetailChars += AllCharacteristicsText.WeaponRange + weaponData.MaxRange + "\n";
            allDetailChars += AllCharacteristicsText.WeaponReloadTime + weaponData.TimeBeetwenFire;
            return allDetailChars;
        }
    }
}
