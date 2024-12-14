using ProjectBase.Core;
using TMPro;
using UnityEngine;

namespace ProjectBase.UI
{
    public class LevelText : MonoBehaviour
    {
        private TextMeshProUGUI _levelDisplayText;
        public TextMeshProUGUI LevelDisplayText => _levelDisplayText ?? GetComponent<TextMeshProUGUI>();

        private void OnEnable()
        {
            if (Managers.Instance == null)
                return;

            SceneController.Instance.OnSceneLoaded.AddListener(SetFakeLevelText);
        }

        private void OnDisable()
        {
            if (Managers.Instance == null)
                return;

            SceneController.Instance.OnSceneLoaded.RemoveListener(SetFakeLevelText);
        }


        private void SetFakeLevelText()
        {
            int fakeLevel = PlayerPrefs.GetInt("FakeLevel", 1);
            LevelDisplayText.SetText("Level " + fakeLevel);
        }
    }
}
