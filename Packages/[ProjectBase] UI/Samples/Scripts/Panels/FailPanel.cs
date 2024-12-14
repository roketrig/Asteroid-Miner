using ProjectBase.Core;

namespace ProjectBase.UI
{
    public class FailPanel : BaseFadePanel
    {
        public bool IsButtonInteracted { get; private set; }

        private void OnEnable()
        {
            if (Managers.Instance == null)
                return;

            SceneController.Instance.OnSceneLoaded.AddListener(OnSceneLoaded);
            GameManager.Instance.OnStageFail.AddListener(ShowPanelAnimated);
        }

        private void OnDisable()
        {
            if (Managers.Instance == null)
                return;

            SceneController.Instance.OnSceneLoaded.RemoveListener(OnSceneLoaded);
            GameManager.Instance.OnStageFail.RemoveListener(ShowPanelAnimated);
        }

        public void RetryButton()
        {
            if (IsButtonInteracted)
                return;

            IsButtonInteracted = true;
            HidePanelAnimated();

            LevelManager.Instance.ReloadLevel();
        }

        private void OnSceneLoaded()
        {
            IsButtonInteracted = false;
            HidePanel();
        }
    }
}
