using GameStates.States.Interfaces;
using SceneLoading;
using Services.CoroutineRunner;
using Services.GameEnd;
using Services.UIFactory;
using Services.WindowsService;
using StaticData.Windows;
using Services.PlayerData;
using Game.PlayerConstructor;
using UI.Base;

namespace GameStates.States
{
    public class LoadGameLevelState : IPayloadedState<string>
    {
        private readonly ISceneLoader sceneLoader;
        private readonly IGameStateMachine gameStateMachine;
        private readonly IUIFactory uiFactory;
        private readonly IWindowsService windowsService;
        private readonly IGameEndService gameEndService;
        private readonly ICoroutineRunner coroutineRunner;
        private readonly IPlayerStaticData playerStaticData;

        private PlayerStaticData playerData;
        private PlayerPartsData playerPartsData;
        private BaseWindow hudWindow;

        public LoadGameLevelState(ISceneLoader sceneLoader,  IGameStateMachine gameStateMachine,   IUIFactory uiFactory, 
            IWindowsService windowsService, IGameEndService gameEndService, ICoroutineRunner coroutineRunner, IPlayerStaticData playerStaticData , IPlayerPartsData playerPartsData)
        {
            this.sceneLoader = sceneLoader;
            this.gameStateMachine = gameStateMachine;
            this.uiFactory = uiFactory;
            this.windowsService = windowsService;
            this.gameEndService = gameEndService;
            this.coroutineRunner = coroutineRunner;
            this.playerStaticData = playerStaticData;
            playerData = (PlayerStaticData)playerStaticData;
            this.playerPartsData = (PlayerPartsData)playerPartsData;
        }
        
        public void Enter(string payload)
        {
            sceneLoader.Load(payload, OnLoaded);
        }

        public void Exit()
        {
            RefContainer.Instance.FinishTrigger.FinishGameEvent -= gameEndService.FinishGame;
            RefContainer.Instance.MainPlayer.PlayerDeathEvent -= LoseGame;
        }

        private void OnLoaded()
        {
            ConstructCar constructor = RefContainer.Instance.MainPlayer.GetComponent<ConstructCar>();
            constructor.Construct(playerData.CurrentCarData);
            playerPartsData.SetCurrentDetails(constructor);
            uiFactory.CreateUIRoot();
            hudWindow = windowsService.Open(WindowsID.HUD);
            RefContainer.Instance.FinishTrigger.FinishGameEvent += gameEndService.FinishGame;
            RefContainer.Instance.MainPlayer.PlayerDeathEvent += LoseGame;
        }

        private void LoseGame()
        {
            hudWindow.Close();
            gameEndService.LoseGame();
        }

        private void EnterGameLoopState() => 
            gameStateMachine.Enter<GameLoopState>();
    }
}