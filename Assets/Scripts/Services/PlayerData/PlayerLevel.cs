using System;
using UnityEngine;

namespace Services.PlayerData
{
    public class PlayerLevel
    {
        public int CurrentLevel { get => currentLevel; }
        public Action<int> NewLevelEvent;
        public Action<float> ChangeExpEvent;

        private int currentLevel;
        private int currentExp;
        private int expToNextLevel;

        private int[] expToNextLevelValues = new int[10] 
        {200, 300, 400, 600, 900, 1600, 2600, 4400, 7000, 982000 };

        public void Load()
        {
            if (PlayerPrefs.HasKey(ConstantVariables.LevelKey))
            {
                currentLevel = PlayerPrefs.GetInt(ConstantVariables.LevelKey);
                currentExp = PlayerPrefs.GetInt(ConstantVariables.CurrentExpKey);
            }
            else
            {
                currentExp = 0;
                currentLevel = 1;
            }
            RecountExpToNextLevel();
            ChangeExpNotify();
        }

        public void Save()
        {
            PlayerPrefs.SetInt(ConstantVariables.LevelKey, CurrentLevel);
            PlayerPrefs.SetInt(ConstantVariables.CurrentExpKey, currentExp);
        }

        public void AddExp(int NewExpCount)
        {
            currentExp += NewExpCount;
            while (currentExp >= expToNextLevel)
            {
                currentLevel++;
                NewLevelEvent?.Invoke(CurrentLevel);
                currentExp -= expToNextLevel;
                RecountExpToNextLevel();
            }

            ChangeExpNotify();
            Save();
        }

        public float GetCurrentRatio()
        {
            return (float)currentExp / expToNextLevel;
        }

        public void Reset()
        {
            currentExp = 0;
            currentLevel = 1;
            RecountExpToNextLevel();
            NewLevelEvent?.Invoke(CurrentLevel);
            ChangeExpNotify();
            Save();
        }

        private void ChangeExpNotify()
        {
            float ratio = GetCurrentRatio();
            ChangeExpEvent?.Invoke(ratio);
        }

        private void RecountExpToNextLevel()
        {
            expToNextLevel = expToNextLevelValues[currentLevel - 1];
        }
    }
}
