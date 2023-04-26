using UnityEngine;
using UI.Base;
using GameStates;
using Services.WindowsService;
using UnityEngine.UI;
using GameStates.States;
using StaticData.Windows;
using Services.PlayerData;
using UnityEngine.Audio;
using TMPro;

namespace UI.Windows.MainMenu
{
    public class UIMainMenuTopPlayerInfo : BaseWindow
    {
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private TextMeshProUGUI premiumMoneyText;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private Image expImage;
        [SerializeField] private Button settingsButton;
        [SerializeField] private AudioMixer mainMixer;

        private IWindowsService windowsService;
        public PlayerStaticData PlayerStaticData { get; private set; }

        public void Construct(IWindowsService windowsService, PlayerStaticData playerStaticData)
        {
            this.windowsService = windowsService;
            this.PlayerStaticData = playerStaticData;
            settingsButton.onClick.AddListener(OpenSettings);
            LoadMusic();

            expImage.fillAmount = this.PlayerStaticData.PlayerLevelData.GetCurrentRatio();
            moneyText.text = this.PlayerStaticData.PlayerMoneyData.MoneyCount.ToString();
            premiumMoneyText.text = this.PlayerStaticData.PlayerPremiumMoneyData.MoneyCount.ToString();
            levelText.text = this.PlayerStaticData.PlayerLevelData.CurrentLevel.ToString();

            this.PlayerStaticData.PlayerMoneyData.MoneyChangedEvent += RefreshMoneyText;
            this.PlayerStaticData.PlayerPremiumMoneyData.MoneyChangedEvent += RefreshPremiumMoneyText;
            this.PlayerStaticData.PlayerLevelData.ChangeExpEvent += RefreshExpCount;
            this.PlayerStaticData.PlayerLevelData.NewLevelEvent += RefreshLevel;
        }

        private void OpenSettings()
        {
            windowsService.Open(WindowsID.MainMenuSettings);
        }

        private void LoadMusic()
        {
            if (PlayerPrefs.HasKey(ConstantVariables.EffectsVolume))
            {
                mainMixer.SetFloat(ConstantVariables.EffectsVolume, PlayerPrefs.GetFloat(ConstantVariables.EffectsVolume));
            }
            if (PlayerPrefs.HasKey(ConstantVariables.MusicVolume))
            {
                mainMixer.SetFloat(ConstantVariables.MusicVolume, PlayerPrefs.GetFloat(ConstantVariables.MusicVolume));
            }
        }

        private void RefreshMoneyText(int newMoney)
        {
            moneyText.text = newMoney.ToString();
        }

        private void RefreshPremiumMoneyText(int newPremiumMoney)
        {
            premiumMoneyText.text = newPremiumMoney.ToString();
        }

        private void RefreshLevel(int newLevel)
        {
            levelText.text = newLevel.ToString();
        }

        private void RefreshExpCount(float expRatio)
        {
            expImage.fillAmount = expRatio;
        }

        protected override void CleanUp()
        {
            base.CleanUp();
            settingsButton.onClick.RemoveListener(OpenSettings);
        }

        private void OnDestroy()
        {
            this.PlayerStaticData.PlayerMoneyData.MoneyChangedEvent -= RefreshMoneyText;
            this.PlayerStaticData.PlayerPremiumMoneyData.MoneyChangedEvent -= RefreshPremiumMoneyText;
            this.PlayerStaticData.PlayerLevelData.ChangeExpEvent -= RefreshExpCount;
            this.PlayerStaticData.PlayerLevelData.NewLevelEvent -= RefreshLevel;
        }
    }
}
