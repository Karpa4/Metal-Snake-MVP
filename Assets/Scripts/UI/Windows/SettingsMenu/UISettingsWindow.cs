using GameStates;
using Services.WindowsService;
using UI.Base;
using UnityEngine;
using UnityEngine.UI;
using GameStates.States;
using Services.PlayerData;
using Services.Sound;
using StaticData.Windows;
using UnityEngine.SceneManagement;

namespace UI.Windows.SettingsMenu
{
    public class UISettingsWindow : BaseWindow
    {
        [SerializeField] private Button backButton;
        [SerializeField] private Button expMoneyButton;
        [SerializeField] private Button resetButton;

        private IWindowsService windowsService;
        private PlayerStaticData playerStaticData;
        private PlayerPartsData playerPartsData;

        private void Awake()
        {
            
        }

        public void Construct(IWindowsService windowsService, PlayerStaticData playerStaticData, PlayerPartsData playerPartsData)
        {
            this.playerPartsData = playerPartsData;
            this.windowsService = windowsService;
            this.playerStaticData = playerStaticData;

            expMoneyButton.onClick.AddListener(AddExpAndMoney);
            resetButton.onClick.AddListener(FullReset);
            backButton.onClick.AddListener(CloseSettings);
        }

        private void CloseSettings()
        {
            var inGameAudioScript = FindObjectOfType<AudioManager>();

            if (inGameAudioScript != null)
            {
                inGameAudioScript.PlayButtonSound();
            }

            else
            {
                var mainMenuAudioScript = FindObjectOfType<ButtonSounds>();
                mainMenuAudioScript.OnClick();
                
            }

            CleanUp();
            Destroy(gameObject);
        }

        private void AddExpAndMoney()
        {
            playerStaticData.PlayerLevelData.AddExp(500);
            playerStaticData.PlayerMoneyData.AddMoney(500);
        }
        
        private void FullReset()
        {
            playerStaticData.FullReset();
            playerPartsData.Reset();
        }

        protected override void CleanUp()
        {
            base.CleanUp();
            expMoneyButton.onClick.RemoveListener(AddExpAndMoney);
            resetButton.onClick.RemoveListener(FullReset);
            backButton.onClick.RemoveListener(CloseSettings);
            Destroy(gameObject);
        }
    }
}
