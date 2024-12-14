using ProjectBase.Core;

namespace ProjectBase.UI
{
    public class InputBlockPanel : BasePanel
    {
        private void OnEnable()
        {
            if (Managers.Instance == null)
                return;

            SceneController.Instance.OnSceneLoaded.AddListener(HidePanel);
            GameManager.Instance.OnStageFail.AddListener(ShowPanel);
            GameManager.Instance.OnStageSuccess.AddListener(ShowPanel);
        }

        private void OnDisable()
        {
            if (Managers.Instance == null)
                return;

            SceneController.Instance.OnSceneLoaded.RemoveListener(HidePanel);
            GameManager.Instance.OnStageFail.RemoveListener(ShowPanel);
            GameManager.Instance.OnStageSuccess.RemoveListener(ShowPanel);
        }
    }
}
