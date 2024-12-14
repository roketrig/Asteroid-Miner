using ProjectBase.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectBase.UI
{
    public class HapticStateButton : MonoBehaviour
    {
        [Header("STATE BUTTON VARIABLES")]
        [SerializeField] private Button _stateButton;
        //[SerializeField] private Sprite _stateButtonOnSprite;
        //[SerializeField] private Sprite _stateButtonOffSprite;
        [SerializeField] private TextMeshProUGUI _stateButtonTextMesh;

        private void OnEnable()
        {
            if (Managers.Instance == null)
                return;

            _stateButton.onClick.AddListener(HapticManager.Instance.ChangeHapticState);
            SceneController.Instance.OnSceneLoaded.AddListener(CheckHapticState);
            EventManager.OnHapticStateChange.AddListener(CheckHapticState);
        }

        private void OnDisable()
        {
            if (Managers.Instance == null)
                return;

            _stateButton.onClick.RemoveListener(HapticManager.Instance.ChangeHapticState);
            SceneController.Instance.OnSceneLoaded.RemoveListener(CheckHapticState);
            EventManager.OnHapticStateChange.RemoveListener(CheckHapticState);
        }

        private void CheckHapticState()
        {
            if (HapticManager.Instance.isHapticActive)
            {
                //_stateButton.image.sprite = _stateButtonOnSprite;
                _stateButton.image.color = Color.green;
                _stateButtonTextMesh.SetText("ON");
                HapticManager.Instance.HapticStatePlayerPrefs(0);
            }
            else
            {
                //_stateButton.image.sprite = _stateButtonOffSprite;
                _stateButton.image.color = Color.red;
                _stateButtonTextMesh.SetText("OFF");
                HapticManager.Instance.HapticStatePlayerPrefs(1);
            }
        }
    }
}
