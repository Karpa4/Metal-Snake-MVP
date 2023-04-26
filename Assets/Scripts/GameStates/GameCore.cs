using SceneLoading;
using Services;
using Services.CoroutineRunner;

namespace GameStates
{
    public class GameCore
    {
        public readonly GameStateMachine StateMachine;

        public GameCore(ICoroutineRunner coroutineRunner, ref AllServices services)
        {
            StateMachine = CreateGameStateMachine(CreateSceneLoader(coroutineRunner), ref services, coroutineRunner);
        }

        private SceneLoader CreateSceneLoader(ICoroutineRunner coroutineRunner)
        {
            return new SceneLoader(coroutineRunner);
        }

        private GameStateMachine CreateGameStateMachine(ISceneLoader sceneLoader, ref AllServices services, ICoroutineRunner coroutineRunner)
        {
            return new GameStateMachine(sceneLoader,ref services, coroutineRunner);
        }
    }
}


