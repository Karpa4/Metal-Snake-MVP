using System;
using System.Collections;
using Bootstrap;
using Services;
using Services.CoroutineRunner;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneLoading
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private Coroutine loadingCoroutine;
        private Scene lastScene;
        private Scene curScene;

        public Action<AsyncOperation> LoadNewSceneEvent;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(string name, Action onLoaded = null)
        {
            if (loadingCoroutine != null)
            {
                _coroutineRunner.StopCoroutine(loadingCoroutine);
            }
            loadingCoroutine = _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));
        }

        public void LoadAdditive(string name, Action onLoaded = null)
        {
            if (loadingCoroutine != null)
            {
                _coroutineRunner.StopCoroutine(loadingCoroutine);
            }
            loadingCoroutine = _coroutineRunner.StartCoroutine(AddScene(name, onLoaded));
        }

        public void UnLoadScene(string newSceneName)
        {
            if (loadingCoroutine != null)
            {
                _coroutineRunner.StopCoroutine(loadingCoroutine);
            }
            Debug.Log(lastScene.name);
            if (lastScene.name != "GameField" && lastScene.name != null)
            {
                SceneManager.UnloadSceneAsync(lastScene);
            }
            loadingCoroutine = _coroutineRunner.StartCoroutine(AddScene(newSceneName));
        }

        private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
        {
            /*
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                yield break;
            }
            */
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);
            if (nextScene != ConstantVariables.MainMenuSceneName)
            {
                LoadNewSceneEvent?.Invoke(waitNextScene);
            }
            while (waitNextScene.isDone == false)
                yield return null;


            onLoaded?.Invoke();
        }

        private IEnumerator AddScene(string nextScene, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                yield break;
            }
            lastScene = curScene;
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);
            curScene = SceneManager.GetSceneByName(nextScene);

            while (waitNextScene.isDone == false)
                yield return null;


            onLoaded?.Invoke();
        }
    }
}
