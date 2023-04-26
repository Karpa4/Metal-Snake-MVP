using GameStates.States.Interfaces;
using SceneLoading;
using Services;
using Services.CoroutineRunner;
using Services.UIFactory;
using Services.StaticData;
using Services.WindowsService;
using Services.PlayerData;
using Services.GameEnd;
using UnityEngine.Audio;
using UnityEngine;

namespace GameStates.States
{
    public class BootstrapState : IState
    {
        private readonly ISceneLoader sceneLoader;
        private readonly IGameStateMachine gameStateMachine;
        private readonly AllServices services;

        public BootstrapState(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader, ref AllServices services, ICoroutineRunner coroutineRunner)
        {
            this.gameStateMachine = gameStateMachine;
            this.sceneLoader = sceneLoader;
            this.services = services;
            RegisterServices(coroutineRunner, gameStateMachine);
        }

        public void Enter()
        {
            gameStateMachine.Enter<MainMenuState>();
        }

        public void Exit()
        {

        }

        private void RegisterServices(ICoroutineRunner coroutineRunner, IGameStateMachine stateMachine)
        {
            RegisterPlayerStaticDataService();
            RegisterStaticDataService();
            RegisterPlayerPartsData();
            RegisterUIFactory(stateMachine);
            RegisterWindowsService();
            RegisterGameEndService(coroutineRunner);
        }

        private void RegisterPlayerStaticDataService()
        {
            IPlayerStaticData playerStaticDataService = new PlayerStaticData();
            services.RegisterSingle(playerStaticDataService);
        }

        private void RegisterPlayerPartsData()
        {
            services.RegisterSingle(new PlayerPartsData());
        }

        private void RegisterGameEndService(ICoroutineRunner coroutineRunner)
        {
            services.RegisterSingle(new GameEndService(
                coroutineRunner,
                services.Single<IWindowsService>(),
                services.Single<IPlayerStaticData>()
                ));
        }

        private void RegisterStaticDataService()
        {
            IStaticDataService service = new StaticDataService();
            service.Load();
            services.RegisterSingle(service);
        }

        private void RegisterUIFactory(IGameStateMachine stateMachine) =>
            services.RegisterSingle(new UIFactory(
                services.Single<IStaticDataService>(),
                services.Single<IPlayerStaticData>(),
                stateMachine,
                services.Single<IPlayerPartsData>(),
                sceneLoader)
            );

        private void RegisterWindowsService() =>
            services.RegisterSingle(new WindowsService(services.Single<IUIFactory>()));
    }
}