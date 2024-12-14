using ProjectBase.Core;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ProjectBase.UI
{
    public class SettingsButton : Button
    {
        public override void OnPointerClick(PointerEventData pointerEventData)
        {
            base.OnPointerClick(pointerEventData);
            EventManager.OnOpenSettingsPanel?.Invoke();
        }
    }
}
