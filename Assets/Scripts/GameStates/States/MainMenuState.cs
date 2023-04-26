using GameStates.States.Interfaces;
using SceneLoading;
using Services.UIFactory;
using Services.WindowsService;
using StaticData.Windows;
using Services.PlayerData;
using UnityEngine;
using Game.PlayerConstructor;

namespace GameStates.States
{
    public class MainMenuState : IState
    {
        private readonly IUIFactory uiFactory;
        private readonly IWindowsService windowsService;
        private readonly ISceneLoader sceneLoader;
        private readonly PlayerPartsData playerPartsData;

        public MainMenuState(IUIFactory uiFactory, IWindowsService windowsService, ISceneLoader sceneLoader, IPlayerPartsData playerPartsData)
        {
            this.uiFactory = uiFactory;
            this.windowsService = windowsService;
            this.sceneLoader = sceneLoader;
            this.playerPartsData = (PlayerPartsData)playerPartsData;
        }

        public void Enter()
        {
            sceneLoader.Load(ConstantVariables.MainMenuSceneName, OnLoaded);
        }

        private void OnLoaded()
        {
            uiFactory.CreateUIRoot();
            ConstructCar constructCarUI = Resources.Load<ConstructCar>(ConstantVariables.UIPlayerPrefabPath);
            ConstructCar instConstructCarUI = Object.Instantiate(constructCarUI);
            playerPartsData.SetupMenuConstructCar(instConstructCarUI);
            windowsService.Open(WindowsID.PlayerTopInfo);
            windowsService.Open(WindowsID.MainMenu);
        }

        public void Exit()
        {

        }
    }
}