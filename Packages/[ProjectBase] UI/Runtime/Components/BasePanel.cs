using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectBase.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class BasePanel : MonoBehaviour
    {
        public UnityEvent OnPanelShown = new UnityEvent();
        public UnityEvent OnPanelHide = new UnityEvent();

        private CanvasGroup _canvasGroup;
        protected CanvasGroup CanvasGroup { get { return (_canvasGroup == null) ? _canvasGroup = GetComponent<CanvasGroup>() : _canvasGroup; } }

        [ButtonGroup("PanelVisibility")]
        public virtual void ShowPanel()
        {
            if (CanvasGroup.alpha > 0)
                return;

            IBasePanelAnimation panelAnimation = GetComponent<IBasePanelAnimation>();

            if (panelAnimation != null)
                panelAnimation.DoShowAnimation();
            else SetPanel(1, true, true);
        }

        [ButtonGroup("PanelVisibility")]
        public virtual void HidePanel()
        {
            if (CanvasGroup.alpha == 0)
                return;

            IBasePanelAnimation panelAnimation = GetComponent<IBasePanelAnimation>();

            if (panelAnimation != null)
                panelAnimation.DoHideAnimation();
            else SetPanel(0, false, false);
        }

        public void SetPanel(float alpha, bool interactable, bool blocksRaycast)
        {
            CanvasGroup.alpha = alpha;
            CanvasGroup.interactable = interactable;
            CanvasGroup.blocksRaycasts = blocksRaycast;
        }

        [ButtonGroup("PanelVisibility")]
        public virtual void TogglePanel()
        {
            if (CanvasGroup.alpha == 0)
                ShowPanel();
            else HidePanel();
        }
    }
}
