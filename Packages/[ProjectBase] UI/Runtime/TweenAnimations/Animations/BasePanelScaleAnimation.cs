using DG.Tweening;
using UnityEngine;

namespace ProjectBase.UI
{
    public class BasePanelScaleAnimation : BasePanelAnimationBase
    {
        public override void DoShowAnimation()
        {
            transform.localScale = Vector3.one;
            transform.DOScale(Vector3.zero, Duration).SetEase(ShowEase).From();
            BasePanel.SetPanel(1, true, true);
        }

        public override void DoHideAnimation()
        {
            transform.localScale = Vector3.one;
            transform.DOScale(Vector3.zero, Duration).SetEase(HideEase).OnComplete(() => BasePanel.SetPanel(0, false, false));
        }
    }
}
