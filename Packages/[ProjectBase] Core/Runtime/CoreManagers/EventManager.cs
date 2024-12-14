using UnityEngine.Events;
using UnityEngine;
namespace ProjectBase.Core
{
    public static class EventManager
    {
        #region DATA EVENTS
        public static UnityEvent OnPlayerDataChange = new UnityEvent();
        public static UnityEvent OnLevelDataChange = new UnityEvent();
        public static UnityEvent OnSoundStateChange = new UnityEvent();
        public static UnityEvent OnHapticStateChange = new UnityEvent();
        public static UnityEvent OnOpenPlayerUpgradePanel = new UnityEvent();
        #endregion

        #region PANEL EVENTS
        public static UnityEvent OnOpenLoadingPanel = new UnityEvent();
        public static UnityEvent OnOpenGamePanel = new UnityEvent();
        public static UnityEvent OnOpenSettingsPanel = new UnityEvent();
        #endregion

        #region INPUT EVENTS
        public static UnityEvent OnInputTaken = new UnityEvent();
        public static UnityEvent<Vector2> OnInputDragged = new UnityEvent<Vector2>();
        public static UnityEvent OnInputReleased = new UnityEvent();
        #endregion
    }
}
