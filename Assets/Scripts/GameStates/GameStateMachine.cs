using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameStates.States.Interfaces;
using System;
using GameStates.States;
using SceneLoading;
using Services;
using Services.PlayerData;
using Services.CoroutineRunner;
using Services.GameEnd;
using Services.UIFactory;
using Services.WindowsService;

namespace GameStates
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(ISceneLoader sceneLoader, ref AllServices services, ICoroutineRunner coroutineRunner) 
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState
                (
                    this,
                    sceneLoader,
                    ref services,
                    coroutineRunner
                ),
                
                [typeof(MainMenuState)] = new MainMenuState
                (
                    services.Single<IUIFactory>(), 
                    services.Single<IWindowsService>(), 
                    sceneLoader,
                    services.Single<IPlayerPartsData>()
                ),

                [typeof(LoadGameLevelState)] = new LoadGameLevelState
                (
                    sceneLoader, 
                    this,
                    services.Single<IUIFactory>(),
                    services.Single<IWindowsService>(),
                    services.Single<IGameEndService>(),
                    coroutineRunner,
                    services.Single<IPlayerStaticData>(),
                    services.Single<IPlayerPartsData>()
                ),
                
                [typeof(GameLoopState)] = new GameLoopState(),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
            Debug.Log(state + " " + (double)Time.deltaTime);
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
          _states[typeof(TState)] as TState;
    }
}

