using ProjectBase.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBase.Core
{
    public class AudioManager : Singleton<AudioManager>
    {
        public List<AudioClip> audioClips;

        private AudioSource _audioSource;
        private AudioSource AudioSource => _audioSource ?? GetComponent<AudioSource>();

        public float randomPercent = 5;

        public bool isSoundActive;
        private int _stateCount;

        private void Start() => GetStateData();

        public void PlaySound(int index)
        {
            if (isSoundActive)
            {
                AudioSource.clip = audioClips[index];
                AudioSource.pitch *= 1 + Random.Range(-randomPercent / 100, randomPercent / 100);

                AudioSource.PlayOneShot(AudioSource.clip);
            }
        }

        public void PlayNormalSound(int index)
        {
            if (isSoundActive)
            {
                AudioSource.clip = audioClips[index];
                AudioSource.pitch = 1;
                AudioSource.PlayOneShot(AudioSource.clip);
            }
        }

        #region DATA/STATES
        public void ChangeSoundState()
        {
            isSoundActive = !isSoundActive;
            EventManager.OnSoundStateChange.Invoke();
        }

        public void SoundStatePlayerPrefs(int value)
        {
            _stateCount = value;
            PlayerPrefs.SetInt(PlayerPrefKeys.SoundState, _stateCount);
        }

        private void GetStateData()
        {
            if (PlayerPrefs.HasKey(PlayerPrefKeys.SoundState))
                _stateCount = PlayerPrefs.GetInt(PlayerPrefKeys.SoundState);
            else
                _stateCount = 0;

            if (_stateCount == 0)
                isSoundActive = true;
            else
                isSoundActive = false;
        }

        #endregion
    }
}

