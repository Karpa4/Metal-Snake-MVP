using UnityEngine;
using Services.CoroutineRunner;
using Services;
using GameStates;
using GameStates.States;

namespace Bootstrap
{
    public class GameBootstrap : MonoBehaviour, ICoroutineRunner
    {
        private GameCore gameCore;
        private AllServices allServices;

        private void Awake()
        {
            allServices = new AllServices();
            gameCore = new GameCore(this, ref allServices);
            gameCore.StateMachine.Enter<BootstrapState>();
            CurrencyImages.Load();
            DontDestroyOnLoad(this);
        }

        private void OnDestroy()
        {
            allServices.Cleanup();
        }
    }
}

