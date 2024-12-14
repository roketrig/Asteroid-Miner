using ProjectBase.Utilities;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace ProjectBase.Core
{
    public class SceneController : Singleton<SceneController>
    {
        #region EVENTS
        [HideInInspector]
        public UnityEvent OnSceneStartedLoading = new UnityEvent();
        [HideInInspector]
        public UnityEvent OnSceneUnloading = new UnityEvent();
        [HideInInspector]
        public UnityEvent OnSceneLoaded = new UnityEvent();
        [HideInInspector]
        public UnityEvent OnSceneUnLoaded = new UnityEvent();
        [HideInInspector]
        public SceneEvent OnSceneInfo = new SceneEvent();
        #endregion

        public bool loadingInProgress { get; private set; }

        public void LoadScene(string scene)
        {
            if (loadingInProgress)
                return;

            StartCoroutine(LoadSceneCo(scene));
        }

        private IEnumerator LoadSceneCo(string sceneName)
        {
            loadingInProgress = true;
            yield return new WaitForSeconds(2);

            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                if (scene.name.Contains("Level"))
                    yield return UnloadSceneCo(scene);
                else if (scene.name.Contains("Test"))
                {
                    sceneName = "Test";
                }
            }

            OnSceneStartedLoading.Invoke();

            yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            var loadedScene = SceneManager.GetSceneByName(sceneName);
            if (loadedScene.name.Contains("Level") || loadedScene.name.Contains("Test"))
            {
                SceneManager.SetActiveScene(loadedScene);
                yield return new WaitForSeconds(0.2f);
                OnSceneLoaded.Invoke();
            }

            OnSceneInfo.Invoke(loadedScene, true);
            loadingInProgress = false;
        }

        public void UnloadScene(string sceneName)
        {
            Scene scene = SceneManager.GetSceneByName(sceneName);
            OnSceneUnloading.Invoke();
            SceneManager.SetActiveScene(SceneManager.GetSceneAt(0));
            StartCoroutine(UnloadSceneCo(scene));
        }

        IEnumerator UnloadSceneCo(Scene scene)
        {
            OnSceneInfo.Invoke(scene, false);
            yield return SceneManager.UnloadSceneAsync(scene.buildIndex);
            OnSceneUnLoaded.Invoke();
        }
    }

    public class SceneEvent : UnityEvent<Scene, bool> { }
}

