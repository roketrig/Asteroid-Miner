using ProjectBase.Core;

namespace ProjectBase.UI
{
    public class SceneLoadingPanel : BaseFadePanel
    {
        protected override float FadeOutDuration => base.FadeOutDuration + 0.5f;

        private void OnEnable()
        {
            if (Managers.Instance == null)
                return;

            ShowPanel();
            LevelManager.Instance.OnLevelFinish.AddListener(ShowPanelAnimated);
            SceneController.Instance.OnSceneLoaded.AddListener(HidePanelAnimated);
        }

        private void OnDisable()
        {
            if (Managers.Instance == null)
                return;

            LevelManager.Instance.OnLevelFinish.RemoveListener(ShowPanelAnimated);
            SceneController.Instance.OnSceneLoaded.RemoveListener(HidePanelAnimated);
        }

    }
}
