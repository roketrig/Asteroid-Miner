using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public Transform[] MineSpawnPoints;
    public GameObject[] Mines;

    private void Start()
    {
        StartCoroutine(SpawnMines());
    }

    private IEnumerator SpawnMines()
    {
        while (true)
        {
            foreach (Transform spawnPoint in MineSpawnPoints)
            {
                if (spawnPoint.childCount == 0)
                {
                    int mineIndex = Random.Range(0, Mines.Length);
                    Instantiate(Mines[mineIndex], spawnPoint.position, spawnPoint.rotation, spawnPoint);
                }
            }
            yield return new WaitForSeconds(20f);
        }
    }
}