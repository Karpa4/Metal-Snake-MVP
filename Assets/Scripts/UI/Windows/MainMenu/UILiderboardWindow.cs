using GameStates;
using Services.WindowsService;
using UI.Base;
using UnityEngine;
using UnityEngine.UI;
using GameStates.States;
using StaticData.Windows;

namespace UI.Windows.MainMenu
{
    public class UILiderboardWindow : BaseWindow
    {
        [SerializeField] private Button backButton;
        [SerializeField] private Button updateButton;

        private IWindowsService windowsService;

        public void Construct(IWindowsService windowsService)
        {
            this.windowsService = windowsService;
            backButton.onClick.AddListener(OpenMainMenu);
            updateButton.onClick.AddListener(UpdateLeaderboard);
        }

        private void OpenMainMenu()
        {
            windowsService.Open(WindowsID.MainMenu);
            CleanUp();
            Destroy(gameObject);
        }

        private void UpdateLeaderboard()
        {

        }

        protected override void CleanUp()
        {
            base.CleanUp();
            backButton.onClick.RemoveListener(OpenMainMenu);
            updateButton.onClick.RemoveListener(UpdateLeaderboard);
        }
    }
}
