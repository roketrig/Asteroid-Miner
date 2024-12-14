using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Kill Counter Data", menuName = "Create Enemy Kill Counter Data")]
public class EnemyKillCounterSO : ScriptableObject
{
    public string CounterName = "TotalEnemiesKilled"; 
    [HideInInspector] public int totalEnemiesKilled;

    public void InitData()
    {
        if (PlayerPrefs.HasKey(CounterName))
        {
            totalEnemiesKilled = PlayerPrefs.GetInt(CounterName);
        }
        else
        {
            totalEnemiesKilled = 0;  
        }

        SaveData();
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt(CounterName, totalEnemiesKilled);
        PlayerPrefs.Save();
    }

    public void IncrementKillCount()
    {
        totalEnemiesKilled++;
        Debug.Log("Total Kills: " + totalEnemiesKilled);
        SaveData();
    }
}
