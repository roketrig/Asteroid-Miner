using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using ProjectBase.Utilities;
using System.IO;

namespace ProjectBase.Core.Editor
{
    public class DataEditor : OdinEditorWindow
    {
        public PlayerData PlayerData;

        [MenuItem("Project Base Settings/Player Data Editor")]
        private static void OpenWindow()
        {
            GetWindow<DataEditor>().Show();
        }

        [Button]
        protected override void Initialize()
        {
            base.Initialize();
            PlayerData = SaveLoadManager.LoadPDP(SavedFileNameHolder.PlayerData, new PlayerData());
            Debug.Log("Initialize All Data!");
        }


        [Button]
        public void SaveData()
        {
            SaveLoadManager.SavePDP(PlayerData, SavedFileNameHolder.PlayerData);
            if (Application.isPlaying)
            {
                GameManager.Instance.PlayerData.UpdateCurrencyData(ExchangeType.Coin, (float)PlayerData.CurrencyData[ExchangeType.Coin]);
                //GameManager.Instance.PlayerData.UpdateCurrencyData(ExchangeType.Diamond, (float)PlayerData.CurrencyData[ExchangeType.Diamond]);
            }
            Debug.Log("Save All Data!");
        }

        [Button]
        public void DeleteData()
        {
            EasySaveClear();
            SaveLoadManager.DeleteFile(SavedFileNameHolder.PlayerData);
            PlayerPrefs.DeleteAll();

            PlayerData = new PlayerData();
            Debug.Log("Delete All Data!");
        }

        private void EasySaveClear()
        {
            DirectoryInfo di = new DirectoryInfo(Application.persistentDataPath);
            foreach (FileInfo file in di.GetFiles())
                file.Delete();
            foreach (DirectoryInfo dir in di.GetDirectories())
                dir.Delete(true);
        }
    }
}
