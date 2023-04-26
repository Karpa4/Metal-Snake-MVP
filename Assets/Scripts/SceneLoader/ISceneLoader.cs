using System;

namespace SceneLoading
{
    public interface ISceneLoader
    {
        void Load(string name, Action onLoaded = null);
        void LoadAdditive(string name, Action onLoaded = null);
        void UnLoadScene(string newScene);
    }
}