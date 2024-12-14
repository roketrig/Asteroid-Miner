using ProjectBase.Core;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectBase.UI
{
    public class SettingsPanel : BasePanel
    {
        [SerializeField] private Button _closeButton;

        private void OnEnable()
        {
            if (Managers.Instance == null)
                return;

            _closeButton.onClick.AddListener(HidePanel);
            EventManager.OnOpenSettingsPanel.AddListener(ShowPanel);
        }

        private void OnDisable()
        {
            if (Managers.Instance == null)
                return;

            _closeButton.onClick.RemoveListener(HidePanel);
            EventManager.OnOpenSettingsPanel.RemoveListener(ShowPanel);
        }
    }
}
