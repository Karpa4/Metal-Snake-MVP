using Services.WindowsService;
using UI.Base;
using UnityEngine.UI;
using UnityEngine;
using GameStates;
using TMPro;
using Game;
using StaticData.Windows;

namespace UI.Windows.Game.HUD
{
    public class UIPlayerHUD : BaseWindow
    {
        [SerializeField] private Button pauseButton;
        [SerializeField] private TextMeshProUGUI carriageCountText;
        [SerializeField] private TextMeshProUGUI additionalAmmoCountText;
        [SerializeField] private Image hpImage;
        [SerializeField] private ArrowController arrowController;
        [SerializeField] private DirectionArrow directionArrow;
        private MainPlayer mainPlayer;
        private CarriageManager carriageManager;
        private AdditionalWeapon additionalWeapon;
        private IWindowsService windowsService;
        private IGameStateMachine gameStateMachine;

        public void Construct(IWindowsService windowsService, IGameStateMachine gameStateMachine)
        {
            this.windowsService = windowsService;
            this.gameStateMachine = gameStateMachine;
            pauseButton.onClick.AddListener(ActivePause);
            mainPlayer = RefContainer.Instance.MainPlayer;
            directionArrow.SetController(mainPlayer.PlayerController);
            additionalWeapon = mainPlayer.GetComponent<AdditionalWeapon>();
            carriageManager = mainPlayer.GetComponent<CarriageManager>();
            int carriagesCount = carriageManager.GetCarriagesCount();
            additionalWeapon.onAmmoChanged += RefreshAdditionalAmmoText;
            RefreshCarriageText(carriagesCount);
            mainPlayer.PlayerHealth.onHealthChanged += RefreshHP;
            carriageManager.OnCarriageChange += RefreshCarriageText;
        }

        private void RefreshCarriageText(int newCount)
        {
            carriageCountText.text = newCount.ToString();
        }

        private void RefreshHP(float ratio)
        {
            hpImage.fillAmount = ratio;
        }

        private void ActivePause()
        {
            RefContainer.Instance.MainPauseManager.PauseOn();
            windowsService.Open(WindowsID.PauseMenu);
        }

        private void OnDestroy()
        {
            CleanUp();
        }

        public override void Close()
        {
            Destroy(gameObject);
        }

        protected override void CleanUp()
        {
            base.CleanUp();
            carriageManager.OnCarriageChange -= RefreshCarriageText;
            mainPlayer.PlayerHealth.onHealthChanged -= RefreshHP;
            additionalWeapon.onAmmoChanged -= RefreshAdditionalAmmoText;
            pauseButton.onClick.RemoveListener(ActivePause);
        }

        private void RefreshAdditionalAmmoText(int newCount) // test
        {
            additionalAmmoCountText.text = newCount.ToString();
        }
    }
}