using ProjectBase.Utilities;
using Lofelt.NiceVibrations;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectBase.Core
{
    public class HapticManager : Singleton<HapticManager>
    {
        [HideInInspector] public bool isHapticActive;
        private int _stateCount;

        public static UnityEvent OnHaptic = new UnityEvent();

        private void Start()
        {
            GetStateData();
        }

        public void Haptic(HapticPatterns.PresetType hapticType)
        {
            if (isHapticActive)
            {
                HapticPatterns.PlayPreset(hapticType);
                OnHaptic.Invoke();
            }
        }

        #region DATA/STATE
        public void ChangeHapticState()
        {
            isHapticActive = !isHapticActive;
            EventManager.OnHapticStateChange.Invoke();
        }

        public void HapticStatePlayerPrefs(int value)
        {
            _stateCount = value;
            PlayerPrefs.SetInt(PlayerPrefKeys.HapticState, _stateCount);
        }

        private void GetStateData()
        {
            if (PlayerPrefs.HasKey(PlayerPrefKeys.HapticState))
                _stateCount = PlayerPrefs.GetInt(PlayerPrefKeys.HapticState);
            else
                _stateCount = 0;

            if (_stateCount == 0)
                isHapticActive = true;
            else
                isHapticActive = false;
        }
        #endregion
    }
}
