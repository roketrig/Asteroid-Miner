using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Asteroid Data", menuName = "Create Asteroid Data")]
public class AsteroidDataSO : ScriptableObject
{
    public List<SpawnedAsteroidData> spawnedAsteroids = new List<SpawnedAsteroidData>();

    public void InitData()
    {
        if (PlayerPrefs.HasKey("SpawnedAsteroids"))
        {
            string savedData = PlayerPrefs.GetString("SpawnedAsteroids");
            spawnedAsteroids = JsonUtility.FromJson<SpawnedAsteroidDataList>(savedData).spawnedAsteroids;
        }
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(new SpawnedAsteroidDataList { spawnedAsteroids = spawnedAsteroids });
        PlayerPrefs.SetString("SpawnedAsteroids", data);
    }

    public void AddSpawnedAsteroid(Vector3 position, Quaternion rotation, int mineIndex)
    {
        SpawnedAsteroidData newAsteroid = new SpawnedAsteroidData
        {
            position = position,
            rotation = rotation,
            mineIndex = mineIndex
        };
        spawnedAsteroids.Add(newAsteroid);
        SaveData();
    }
}

[Serializable]
public class SpawnedAsteroidData
{
    public Vector3 position;
    public Quaternion rotation;
    public int mineIndex; 
}

[Serializable]
public class SpawnedAsteroidDataList
{
    public List<SpawnedAsteroidData> spawnedAsteroids;
}
