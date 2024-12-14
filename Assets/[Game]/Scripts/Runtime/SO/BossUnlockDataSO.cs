using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Boss Unlock Data", menuName = "Create Boss Unlock Data")]
public class BossUnlockDataSO : ScriptableObject
{
    public string bossUnlockKey = "BossUnlocked";
    [HideInInspector] public bool isBossUnlocked;

    public void InitData()
    {
        isBossUnlocked = PlayerPrefs.GetInt(bossUnlockKey, 0) == 1; // 0 yok, 1 var
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt(bossUnlockKey, isBossUnlocked ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void UnlockBoss()
    {
        isBossUnlocked = true;
        SaveData();
    }
}
