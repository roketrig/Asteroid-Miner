using ProjectBase.Core;
using ProjectBase.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUpgradePanel : BaseFadePanel
{
    [SerializeField] private Button _closeButton;
    private void OnEnable()
    {
        if(Managers.Instance == null)
                return;
        EventManager.OnOpenPlayerUpgradePanel.AddListener(ShowPanelAnimated);
        _closeButton.onClick.AddListener(HidePanelAnimated);
    }

    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;
        EventManager.OnOpenPlayerUpgradePanel.RemoveListener(ShowPanelAnimated);
        _closeButton.onClick.RemoveListener(HidePanelAnimated);
    }
}
