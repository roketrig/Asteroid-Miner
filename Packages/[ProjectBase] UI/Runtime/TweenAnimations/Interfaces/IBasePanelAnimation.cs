using DG.Tweening;

namespace ProjectBase.UI
{
    public interface IBasePanelAnimation
    {
        public float Duration { get; set; }
        public Ease ShowEase { get; set; }
        public Ease HideEase { get; set; }

        void DoShowAnimation();
        void DoHideAnimation();
    }
}
