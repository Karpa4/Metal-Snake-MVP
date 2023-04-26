using System;
using GameStates;
using Services.StaticData;
using Services.WindowsService;
using StaticData.Windows;
using UI.Base;
using UI.Windows.Game.EndMenu;
using UI.Windows.Game.HUD;
using UI.Windows.Game.PauseMenu;
using UI.Windows.MainMenu;
using UI.Windows.SettingsMenu;
using Services.PlayerData;
using UnityEngine;
using Object = UnityEngine.Object;
using UI.Windows.Garage;
using UI.Windows.Shop;
using SceneLoading;

namespace Services.UIFactory
{
    public class UIFactory : IUIFactory
    {
        private readonly IStaticDataService staticDataService;
        private readonly IGameStateMachine gameStateMachine;
        private readonly ISceneLoader sceneLoader;
        private PlayerPartsData playerPartsData;
        private PlayerStaticData playerStaticData;
        private Transform uiRoot;

        public UIFactory(IStaticDataService staticDataService, IPlayerStaticData playerStaticData, IGameStateMachine gameStateMachine, IPlayerPartsData playerPartsData, ISceneLoader sceneLoader)
        {
            this.playerPartsData = (PlayerPartsData)playerPartsData;
            this.playerStaticData = (PlayerStaticData)playerStaticData;
            this.staticDataService = staticDataService;
            this.gameStateMachine = gameStateMachine;
            this.sceneLoader = sceneLoader;
        }

        public void CreateUIRoot()
        {
            GameObject prefab = Resources.Load<GameObject>(ConstantVariables.UIRootPath);
            Transform spawnedUiRoot = Object.Instantiate(prefab).transform;
            uiRoot = spawnedUiRoot;
        }

        public BaseWindow Create(WindowsID id, IWindowsService windowsService)
        {
            WindowInstantiateData config = LoadWindowInstantiateData(id);

            switch (id)
            {
                case WindowsID.MainMenu:
                    return CreateMainMenuWindow(sceneLoader, config, gameStateMachine, windowsService, playerStaticData);
                case WindowsID.MainMenuSettings:
                    return CreateMainMenuSettingsWindow(config, windowsService, playerStaticData);
                case WindowsID.Garage:
                    return CreateGarageWindow(config, windowsService, playerStaticData);
                case WindowsID.Shop:
                    return CreateShopWindow(config, windowsService, playerStaticData, playerPartsData);
                case WindowsID.PlayerTopInfo:
                    return CreateMainMenuTopInfoWindow(config, windowsService, playerStaticData);
                case WindowsID.HUD:
                    return CreatePlayerHUD(config, windowsService, gameStateMachine);
                case WindowsID.GameEnd:
                    return CreateGameEndWindow(config, gameStateMachine);
                case WindowsID.PauseMenu:
                    return CreateGamePauseMenuWindow(config, gameStateMachine, windowsService);
                default:
                    throw new ArgumentOutOfRangeException(nameof(id), id, null);
            }
        }

        private BaseWindow CreateMainMenuTopInfoWindow(WindowInstantiateData config, IWindowsService windowsService, PlayerStaticData playerStaticData)
        {
            BaseWindow window = InstantiateWindow(config);
            ((UIMainMenuTopPlayerInfo)window).Construct(windowsService, playerStaticData);
            return window;
        }

        private BaseWindow CreateShopWindow(WindowInstantiateData config, IWindowsService windowsService, PlayerStaticData playerStaticData, PlayerPartsData playerPartsData)
        {
            BaseWindow window = InstantiateWindow(config);
            ((UIShopWindow)window).Construct(windowsService, playerStaticData, playerPartsData);
            return window;
        }

        private BaseWindow CreateGarageWindow(WindowInstantiateData config, IWindowsService windowsService, PlayerStaticData playerStaticData)
        {
            BaseWindow window = InstantiateWindow(config);
            ((UIGarageWindow)window).Construct(windowsService, playerStaticData, playerPartsData);
            return window;
        }

        private BaseWindow CreateMainMenuWindow(ISceneLoader sceneLoader, WindowInstantiateData config, IGameStateMachine gameStateMachine,
      IWindowsService windowsService, PlayerStaticData playerStaticData)
        {
            BaseWindow window = InstantiateWindow(config);
            ((UIMainMenuWindow)window).Construct(sceneLoader, gameStateMachine, windowsService, playerStaticData);
            return window;
        }

        private BaseWindow CreateGameEndWindow(WindowInstantiateData config, IGameStateMachine gameStateMachine)
        {
            BaseWindow window = InstantiateWindow(config);
            ((UIGameEndWindow)window).Construct(gameStateMachine);
            return window;
        }

        private BaseWindow CreateGamePauseMenuWindow(WindowInstantiateData config, IGameStateMachine gameStateMachine, IWindowsService windowsService)
        {
            BaseWindow window = InstantiateWindow(config);
            ((UIGamePauseWindow)window).Construct(gameStateMachine, windowsService);
            return window;
        }

        private BaseWindow CreateMainMenuSettingsWindow(WindowInstantiateData config, IWindowsService windowsService, PlayerStaticData playerStaticData)
        {
            BaseWindow window = InstantiateWindow(config);
            ((UISettingsWindow)window).Construct(windowsService, playerStaticData, playerPartsData);
            return window;
        }

        private BaseWindow CreatePlayerHUD(WindowInstantiateData config, IWindowsService windowsService, IGameStateMachine gameStateMachine)
        {
            BaseWindow window = InstantiateWindow(config);
            ((UIPlayerHUD)window).Construct(windowsService, gameStateMachine);
            return window;
        }

        private BaseWindow InstantiateWindow(WindowInstantiateData config) =>
          Object.Instantiate(config.Window, uiRoot);

        private WindowInstantiateData LoadWindowInstantiateData(WindowsID id)
        {
            return staticDataService.ForWindow(id);
        }
    }
}