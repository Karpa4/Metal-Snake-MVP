using System;
using UnityEngine;

namespace Services.PlayerData
{
    public class PlayerMoney
    {
        private string playerPrefsKey;

        public int MoneyCount { get; private set; }
        public event Action<int> MoneyChangedEvent;

        public PlayerMoney(string playerPrefsKey, int startValue)
        {
            this.playerPrefsKey = playerPrefsKey;
            if (PlayerPrefs.HasKey(this.playerPrefsKey))
            {
                MoneyCount = PlayerPrefs.GetInt(this.playerPrefsKey);
            }
            else
            {
                MoneyCount = startValue;
            }
        }

        public void Save()
        {
            PlayerPrefs.SetInt(playerPrefsKey, MoneyCount);
        }

        public void AddMoney(int addedMoneyCount)
        {
            MoneyCount += addedMoneyCount;
            MoneyChangedEvent?.Invoke(MoneyCount);
            Save();
        }

        public void SpendMoney(int spentMoneyCount)
        {
            MoneyCount -= spentMoneyCount;
            MoneyChangedEvent?.Invoke(MoneyCount);
            Save();
        }

        public void Reset()
        {
            if (playerPrefsKey == ConstantVariables.MoneyKey)
            {
                MoneyCount = ConstantVariables.StartMoneyCount;
            }
            else
            {
                MoneyCount = ConstantVariables.StartPremiumMoneyCount;
            }
            AddMoney(0);
        }
    }
}
