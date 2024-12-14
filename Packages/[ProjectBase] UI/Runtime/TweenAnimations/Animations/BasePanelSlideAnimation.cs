using DG.Tweening;
using UnityEngine;

namespace ProjectBase.UI
{
    public class BasePanelSlideAnimation : BasePanelAnimationBase
    {
        private enum Direction { Left = -1, Right = 1 }

        [SerializeField]
        private Direction direction = Direction.Left;

        public override void DoHideAnimation()
        {
            transform.DOLocalMoveX(transform.localPosition.x + ((Screen.width / 2) * (int)direction), Duration).SetEase(HideEase).OnComplete(() => BasePanel.SetPanel(0, false, false));
        }

        public override void DoShowAnimation()
        {
            BasePanel.SetPanel(1, true, true);
            transform.DOLocalMoveX(0, Duration).SetEase(ShowEase);
        }
    }
}
