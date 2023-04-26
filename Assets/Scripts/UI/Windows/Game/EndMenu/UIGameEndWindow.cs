using GameStates;
using GameStates.States;
using UI.Base;
using UnityEngine;
using UnityEngine.UI;
using Services.WindowsService;
using TMPro;

namespace UI.Windows.Game.EndMenu
{
    public class UIGameEndWindow : BaseWindow
    {
        [SerializeField] private Button menuButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private Sprite loseSprite;
        [SerializeField] private Sprite winSprite;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private UIGameEndWindowView view;
        [SerializeField] private Image mainGameEndImage;

        private IGameStateMachine gameStateMachine;
        private IWindowsService windowsService;

        public void Construct(IGameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
            menuButton.onClick.AddListener(ExitToMenu);
            restartButton.onClick.AddListener(RestartScene);
        }

        public void SetLoseScreen()
        {
            titleText.text = "Вы проиграли";
            mainGameEndImage.sprite = loseSprite;
        }

        public void DisplayStatistic(int carriagesCount, int rewardExpCount, int rewardMoneyCount)
        {
            titleText.text = "Уровень пройден!";
            view.DisplayStatistic(carriagesCount, rewardExpCount, rewardMoneyCount);
        }

        private void RestartScene()
        {
            gameStateMachine.Enter<LoadGameLevelState, string>(ConstantVariables.GameSceneName);
            CleanUp();
        }

        private void ExitToMenu()
        {
            gameStateMachine.Enter<MainMenuState>();
            CleanUp();
        }

        protected override void CleanUp()
        {
            base.CleanUp();
            restartButton.onClick.RemoveListener(RestartScene);
            menuButton.onClick.RemoveListener(ExitToMenu);
        }
    }
}