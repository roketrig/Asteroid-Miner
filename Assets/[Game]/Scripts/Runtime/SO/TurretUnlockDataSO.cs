using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret Unlock Data", menuName = "Create Turret Unlock Data")]
public class TurretUnlockDataSO : ScriptableObject
{
    public string turretUnlockKey = "TurretUnlocked";
    [HideInInspector] public bool isTurretUnlocked;

    public void InitData()
    {
        isTurretUnlocked = PlayerPrefs.GetInt(turretUnlockKey, 0) == 1; 
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt(turretUnlockKey, isTurretUnlocked ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void UnlockTurret()
    {
        isTurretUnlocked = true;
        SaveData();
    }
}
