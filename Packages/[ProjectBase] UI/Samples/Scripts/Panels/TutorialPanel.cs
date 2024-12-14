using ProjectBase.Core;

namespace ProjectBase.UI
{
    public class TutorialPanel : BasePanel
    {
        private void OnEnable()
        {
            if (Managers.Instance == null)
                return;

            SceneController.Instance.OnSceneLoaded.AddListener(ShowPanel);
            LevelManager.Instance.OnLevelStart.AddListener(HidePanel);
        }

        private void OnDisable()
        {
            if (Managers.Instance == null)
                return;

            SceneController.Instance.OnSceneLoaded.RemoveListener(ShowPanel);
            LevelManager.Instance.OnLevelStart.RemoveListener(HidePanel);
        }
    }
}
