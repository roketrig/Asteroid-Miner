using ProjectBase.Core;

namespace ProjectBase.UI
{
    public class InitializePanel : BasePanel
    {
        private void Start()
        {
            ShowPanel();
        }

        private void OnEnable()
        {
            if (Managers.Instance == null)
                return;

            SceneController.Instance.OnSceneLoaded.AddListener(HidePanel);
        }

        private void OnDisable()
        {
            if (Managers.Instance == null)
                return;

            SceneController.Instance.OnSceneLoaded.RemoveListener(HidePanel);
        }
    }
}
