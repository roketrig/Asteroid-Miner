using DG.Tweening;
using UnityEngine;

namespace ProjectBase.UI
{
    [RequireComponent(typeof(BasePanel))]
    public abstract class BasePanelAnimationBase : MonoBehaviour, IBasePanelAnimation
    {
        private BasePanel basePanel;
        protected BasePanel BasePanel { get { return basePanel == null ? basePanel = GetComponent<BasePanel>() : basePanel; } }

        [SerializeField] private float duration = 1;
        public float Duration { get => duration; set => duration = value; }

        [SerializeField] private Ease showEase = Ease.OutBack;
        public Ease ShowEase { get => showEase; set => showEase = value; }

        [SerializeField] private Ease hideEase = Ease.InBack;
        public Ease HideEase { get => hideEase; set => hideEase = value; }

        protected virtual void OnEnable()
        {
            BasePanel.OnPanelShown.AddListener(DoShowAnimation);
            BasePanel.OnPanelHide.AddListener(DoHideAnimation);
        }

        protected virtual void OnDisable()
        {
            BasePanel.OnPanelShown.RemoveListener(DoShowAnimation);
            BasePanel.OnPanelHide.RemoveListener(DoHideAnimation);
        }

        public abstract void DoHideAnimation();
        public abstract void DoShowAnimation();
    }
}
