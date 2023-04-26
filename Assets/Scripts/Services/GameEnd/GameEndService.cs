using Services.WindowsService;
using StaticData.Windows;
using UI.Windows.Game.EndMenu;
using Services.PlayerData;
using Services.CoroutineRunner;
using UnityEngine;
using System.Collections;

namespace Services.GameEnd
{
    public class GameEndService : IGameEndService
    {
        private readonly IWindowsService windowsService;
        private readonly ICoroutineRunner coroutineRunner;
        private readonly PlayerStaticData playerStaticData;

        public GameEndService(ICoroutineRunner coroutineRunner, IWindowsService windowsService, IPlayerStaticData playerStaticData)
        {
            this.coroutineRunner = coroutineRunner;
            this.windowsService = windowsService;
            this.playerStaticData = (PlayerStaticData)playerStaticData;
        }
        
        public void FinishGame()
        {
            GameObject mainPlayer = RefContainer.Instance.MainPlayer.gameObject;
            mainPlayer.GetComponentInChildren<AudioManager>().PlaySound
                (AudioManager.Sound.LevelCompleted, mainPlayer.GetComponent<AudioSource>());
            RefContainer.Instance.MainPauseManager.PauseOn();
            UIGameEndWindow gameEndWindow = GameEndWindow();
            int carriagesCount = mainPlayer.GetComponent<CarriageManager>().GetCarriagesCount();
            //mainPlayer.SetActive(false);
            int expReward = ConstantVariables.RewardExpPerCarriage + carriagesCount * ConstantVariables.RewardExpPerCarriage;
            int moneyReward = ConstantVariables.RewardMoneyPerCarriage + carriagesCount * ConstantVariables.RewardMoneyPerCarriage;
            gameEndWindow.DisplayStatistic(carriagesCount, expReward, moneyReward);
            playerStaticData.PlayerMoneyData.AddMoney(moneyReward);
            playerStaticData.PlayerLevelData.AddExp(expReward);

        }

        private IEnumerator ActiveLoseScreen()
        {
            yield return new WaitForSeconds(ConstantVariables.TimeBetweenDeathAndActiveLoseScreen);
            UIGameEndWindow gameEndWindow = GameEndWindow();
            RefContainer.Instance.MainPauseManager.PauseOn();
            gameEndWindow.SetLoseScreen();
            RefContainer.Instance.MainPlayer.GetComponentInChildren<AudioManager>().PlaySound
                (AudioManager.Sound.GameOver, RefContainer.Instance.MainPlayer.GetComponent<AudioSource>());
        }

        public void LoseGame()
        {
            coroutineRunner.StartCoroutine(ActiveLoseScreen());
        }

        private UIGameEndWindow GameEndWindow() =>
          ((UIGameEndWindow)windowsService.Open(WindowsID.GameEnd));
    }
}