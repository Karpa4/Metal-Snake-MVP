namespace UI.Windows.Garage
{
    public class AllCharacteristicsText
    {
        public const string Acceleration = "% к ускорению";
        public const string SlowCarriage = "% к замедлению от количества прицепов  ";
        public const string TurnSpeedAcceleration = "% к скорости поворота";
        public const string DamageToEnemies = "% урона противникам при столкновении с игроком";
        public const string DamageToPlayerFromEnem = "% урона от столкновений с противниками";
        public const string DamageToPlayerFromObst = "% урона от столкновений с препятствиями";
        public const string WeaponRange = "Дальность  ";
        public const string WeaponDamage = "Урон  ";
        public const string WeaponReloadTime = "Перезарядка  ";

        public static string GetCharacteristicString(float value, string name)
        {
            string detailCharacteristic = string.Empty;
            if (value != 1)
            {
                float characteristicView = (value - 1) * 100;
                if (characteristicView >= 0)
                {
                    detailCharacteristic = "+";
                }
                detailCharacteristic = detailCharacteristic + characteristicView + name + "\n";
            }
            return detailCharacteristic;
        }

    }
}
