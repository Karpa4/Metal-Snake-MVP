using Services.WindowsService;
using GameStates;
using UI.Base;
using UnityEngine;
using UnityEngine.UI;
using GameStates.States;
using StaticData.Windows;

namespace UI.Windows.Game.PauseMenu
{
    public class UIGamePauseWindow : BaseWindow
    {
        [SerializeField] private Button continueButton;
        [SerializeField] private Button mainMenuButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button restartButton;
        private AudioManager audioScript;

        private IGameStateMachine gameStateMachine;
        private IWindowsService windowsService;

        private void Awake()
        {
            audioScript = FindObjectOfType<AudioManager>();
        }

        public void Construct(IGameStateMachine gameStateMachine, IWindowsService windowsService)
        {
            this.windowsService = windowsService;
            this.gameStateMachine = gameStateMachine;
            continueButton.onClick.AddListener(Continue);
            mainMenuButton.onClick.AddListener(GoToMainMenu);
            settingsButton.onClick.AddListener(OpenSettings);
            restartButton.onClick.AddListener(RestartLevel);
        }

        private void Continue()
        {
            audioScript.PlayButtonSound();
            RefContainer.Instance.MainPauseManager.PauseOff();
            CleanUp();
            Destroy(gameObject);
        }

        private void OpenSettings()
        {
            audioScript.PlayButtonSound();
            windowsService.Open(WindowsID.MainMenuSettings);
        }

        private void RestartLevel()
        {
            audioScript.PlayButtonSound();
            gameStateMachine.Enter<LoadGameLevelState, string>(ConstantVariables.GameSceneName);
            CleanUp();
        }

        private void GoToMainMenu()
        {
            audioScript.PlayButtonSound();
            gameStateMachine.Enter<MainMenuState>();
            CleanUp();
        }

        protected override void CleanUp()
        {
            base.CleanUp();
            continueButton.onClick.RemoveListener(Continue);
            mainMenuButton.onClick.RemoveListener(GoToMainMenu);
            settingsButton.onClick.RemoveListener(OpenSettings);
            restartButton.onClick.RemoveListener(RestartLevel);
        }
    }
}