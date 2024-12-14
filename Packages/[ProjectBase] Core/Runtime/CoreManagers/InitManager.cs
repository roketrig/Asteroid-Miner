using System.Collections;
using UnityEngine.SceneManagement;
using ProjectBase.Utilities;

namespace ProjectBase.Core
{
    public class InitManager : Singleton<InitManager>
    {
        private void Start()
        {
            Invoke("LoadMenuScene", 0.5f);
        }

        public void LoadMenuScene()
        {
            StartCoroutine(LoadMenuSceneCo());
        }

        private IEnumerator LoadMenuSceneCo()
        {
            yield return SceneManager.LoadSceneAsync("Scene_UI", LoadSceneMode.Additive);
            LevelManager.Instance.LoadLastLevel();
            Destroy(gameObject);
        }
    }
}
