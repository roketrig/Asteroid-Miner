using DG.Tweening;
using UnityEngine;

namespace ProjectBase.UI
{
    public class BasePanelVerticalSlideAnimation : BasePanelAnimationBase
    {
        private enum Direction { Up = 1, Down = -1 }

        [SerializeField]
        private Direction direction = Direction.Up;

        public override void DoHideAnimation()
        {
            transform.DOLocalMoveY(transform.localPosition.y + ((Screen.width) * (int)direction), Duration).SetEase(HideEase).OnComplete(() => BasePanel.SetPanel(0, false, false));
        }

        public override void DoShowAnimation()
        {
            BasePanel.SetPanel(1, true, true);
            transform.DOLocalMoveY(0, Duration).SetEase(ShowEase);
        }
    }
}
