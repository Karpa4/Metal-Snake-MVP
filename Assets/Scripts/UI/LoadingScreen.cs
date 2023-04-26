using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using SceneLoading;

namespace UI.Windows.MainMenu
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private PlayerHints allHints;
        [SerializeField] private Transform hintParentTransform;
        [SerializeField] private Button openNewSceneButton;
        [SerializeField] private GameObject loadingAnim;
        [SerializeField] private GameObject infoText;

        private AsyncOperation operation;
        private SceneLoader sceneLoader;

        public void ChooseRandomHint(int playerLevel, SceneLoader sceneLoader)
        {
            this.sceneLoader = sceneLoader;
            sceneLoader.LoadNewSceneEvent += GetAsyncOperation;
            int count = allHints.LevelHints.Count;
            while (true)
            {
                int index = Random.Range(0, count);
                if (allHints.LevelHints[index].MinLevel <= playerLevel)
                {
                    Instantiate(allHints.LevelHints[index].Hint, hintParentTransform);
                    break;
                }
            }
        }

        private void GetAsyncOperation(AsyncOperation operation)
        {
            operation.allowSceneActivation = false;
            this.operation = operation;
            sceneLoader.LoadNewSceneEvent -= GetAsyncOperation;
            StartCoroutine(CheckCoroutine());
        }

        private IEnumerator CheckCoroutine()
        {
            while (operation.progress < 0.9f)
            {
                yield return new WaitForSeconds(0.1f);
            }

            loadingAnim.SetActive(false);
            infoText.SetActive(true);
            openNewSceneButton.onClick.AddListener(OpenScene);
        }

        private void OpenScene()
        {
            openNewSceneButton.onClick.RemoveListener(OpenScene);
            operation.allowSceneActivation = true;
        }
    }
}
