using ProjectBase.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectBase.UI
{
    public class SoundStateButton : MonoBehaviour
    {
        [Header("SOUND IMAGE VARIABLES")]
        [SerializeField] private Image _soundMainImage;
        [SerializeField] private Sprite _soundOnSprite;
        [SerializeField] private Sprite _soundOffSprite;
        [Space]
        [Header("STATE BUTTON VARIABLES")]
        [SerializeField] private Button _stateButton;
        //[SerializeField] private Sprite _stateButtonOnSprite;
        //[SerializeField] private Sprite _stateButtonOffSprite;
        [SerializeField] private TextMeshProUGUI _stateButtonTextMesh;

        private void OnEnable()
        {
            if (Managers.Instance == null)
                return;

            _stateButton.onClick.AddListener(AudioManager.Instance.ChangeSoundState);
            SceneController.Instance.OnSceneLoaded.AddListener(CheckSoundState);
            EventManager.OnSoundStateChange.AddListener(CheckSoundState);
        }

        private void OnDisable()
        {
            if (Managers.Instance == null)
                return;

            _stateButton.onClick.RemoveListener(AudioManager.Instance.ChangeSoundState);
            SceneController.Instance.OnSceneLoaded.RemoveListener(CheckSoundState);
            EventManager.OnSoundStateChange.RemoveListener(CheckSoundState);
        }

        private void CheckSoundState()
        {
            if (AudioManager.Instance.isSoundActive)
            {
                _soundMainImage.sprite = _soundOnSprite;
                //_stateButton.image.sprite = _stateButtonOnSprite;
                _stateButton.image.color = Color.green;
                _stateButtonTextMesh.SetText("ON");
                AudioManager.Instance.SoundStatePlayerPrefs(0);
            }
            else
            {
                _soundMainImage.sprite = _soundOffSprite;
                //_stateButton.image.sprite = _stateButtonOffSprite;
                _stateButton.image.color = Color.red;
                _stateButtonTextMesh.SetText("OFF");
                AudioManager.Instance.SoundStatePlayerPrefs(1);
            }
        }
    }
}
