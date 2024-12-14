using ProjectBase.Core;
using UnityEngine;

namespace ProjectBase.UI
{
    public class SuccesPanel : BaseFadePanel
    {
        public bool IsButtonInteracted { get; private set; }

        private void OnEnable()
        {
            if (Managers.Instance == null)
                return;

            SceneController.Instance.OnSceneLoaded.AddListener(OnSceneLoaded);
            GameManager.Instance.OnStageSuccess.AddListener(ShowPanelAnimated);
        }

        private void OnDisable()
        {
            if (Managers.Instance == null)
                return;

            SceneController.Instance.OnSceneLoaded.RemoveListener(OnSceneLoaded);
            GameManager.Instance.OnStageSuccess.RemoveListener(ShowPanelAnimated);
        }

        public void NextButton()
        {
            if (IsButtonInteracted)
                return;

            IsButtonInteracted = true;
            HidePanelAnimated();

            SetFakeLevel();
            LevelManager.Instance.LoadNextLevel();
        }

        private void OnSceneLoaded()
        {
            IsButtonInteracted = false;
            HidePanel();
        }

        private void SetFakeLevel()
        {
            int fakeLevel = PlayerPrefs.GetInt("FakeLevel", 1);
            fakeLevel++;
            PlayerPrefs.SetInt("FakeLevel", fakeLevel);
        }
    }
}
