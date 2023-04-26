using UnityEngine;

namespace Services.PlayerData
{
    public class PlayerStaticData : IPlayerStaticData
    {
        public PlayerLevel PlayerLevelData { get; private set; }
        public PlayerMoney PlayerMoneyData { get; private set; }
        public PlayerMoney PlayerPremiumMoneyData { get; private set; }
        public CarData CurrentCarData { get; private set; }

        public void FullReset()
        {
            PlayerLevelData.Reset();
            PlayerMoneyData.Reset();
            PlayerPremiumMoneyData.Reset();
        }

        public PlayerStaticData()
        {
            PlayerLevelData = new PlayerLevel();
            PlayerLevelData.Load();
            PlayerMoneyData = new PlayerMoney(ConstantVariables.MoneyKey, ConstantVariables.StartMoneyCount);
            PlayerPremiumMoneyData = new PlayerMoney(ConstantVariables.PremiumMoneyKey, ConstantVariables.StartPremiumMoneyCount);
            CurrentCarData = Resources.Load<CarData>(ConstantVariables.PlayerCarDataPath);
        }
    }
}
