using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using ProjectBase.Utilities;

namespace ProjectBase.Core
{
    public class LevelManager : Singleton<LevelManager>
    {
        [BoxGroup("Level Data")]
        [SerializeField]
        [InlineEditor]
        public LevelData LevelData;

        public Level CurrentLevel { get { return LevelData.Levels[LevelIndex]; } }

        [HideInInspector]
        public UnityEvent OnLevelStart = new UnityEvent();
        [HideInInspector]
        public UnityEvent OnLevelFinish = new UnityEvent();

        bool isLevelStarted;
        [ReadOnly]
        [ShowInInspector]
        public bool IsLevelStarted { get { return isLevelStarted; } set { isLevelStarted = value; } }

        public int LevelIndex
        {
            get
            {
                int level = PlayerPrefs.GetInt(PlayerPrefKeys.LastLevel, 0);
                if (level > LevelData.Levels.Count - 1)
                {
                    level = 0;
                    GameManager.Instance.GameConfig.IsLooping = true;
                }

                if (GameManager.Instance.GameConfig.IsLooping)
                {
                    while (LevelData.Levels[level].LevelTypes.Contains(LevelType.Tutorial))
                        level++;
                }

                return level;
            }
            set
            {
                PlayerPrefs.SetInt(PlayerPrefKeys.LastLevel, value);
            }
        }

        [Button]
        public void ReloadLevel()
        {
            FinishLevel();
            SceneController.Instance.LoadScene(CurrentLevel.LoadLevelID);
        }

        public void LoadLastLevel()
        {
            FinishLevel();
            SceneController.Instance.LoadScene(CurrentLevel.LoadLevelID);
        }

        [Button]
        public void LoadNextLevel()
        {
            FinishLevel();

            LevelIndex++;
            if (LevelIndex > LevelData.Levels.Count - 1)
            {
                LevelIndex = 0;
            }

            SceneController.Instance.LoadScene(CurrentLevel.LoadLevelID);
        }

        [Button]
        public void LoadPreviousLevel()
        {
            FinishLevel();

            LevelIndex--;
            if (LevelIndex <= -1)
            {
                LevelIndex = LevelData.Levels.Count - 1;

            }

            SceneController.Instance.LoadScene(CurrentLevel.LoadLevelID);
        }

        [Button]
        public void StartLevel()
        {
            if (IsLevelStarted)
                return;
            IsLevelStarted = true;
            OnLevelStart.Invoke();
        }

        public void FinishLevel()
        {
            IsLevelStarted = false;
            OnLevelFinish.Invoke();
        }
    }
}
