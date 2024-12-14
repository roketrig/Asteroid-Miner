using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Unlock Data", menuName = "Create Unlock Data")]
public class UnlockDataSO : ScriptableObject
{
    public string unlockKey = "ItemUnlocked"; 
    [HideInInspector] public bool isUnlocked;  

    public void InitData()
    {
        isUnlocked = PlayerPrefs.GetInt(unlockKey, 0) == 1; 
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt(unlockKey, isUnlocked ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void UnlockItem()
    {
        isUnlocked = true;
        SaveData();
    }
}
