using ProjectBase.Core;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Panel_UpgradeButton : Button
{
    public override void OnPointerClick(PointerEventData pointerEventData)
    {
        base.OnPointerClick(pointerEventData);
        EventManager.OnOpenPlayerUpgradePanel?.Invoke();
    }
}
