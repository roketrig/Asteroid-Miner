using DG.Tweening;
using ProjectBase.Core;
using System.Collections.Generic;
using UnityEngine;

public class SellProcess : MonoBehaviour
{
    private SellPoint sellPoint;
    private float totalValue = 0f;
    public GameObject CoinPref;
    private List<GameObject> spawnedCoins = new List<GameObject>(); // Üretilen coinlerin listesi
    private void Start()
    {
        sellPoint = FindObjectOfType<SellPoint>();
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Customer"))
        {
            SpawnItems((int)totalValue);
        }
    }

    private void SpawnItems(int numberOfItems)
    {
        for (int i = 0; i < numberOfItems; i++)
        {
            Vector3 spawnPosition = GetSpawnPosition(i);

            GameObject newItem = InstantiateItemPrefab(spawnPosition);
            spawnedCoins.Add(newItem); 
        }
    }


    private GameObject InstantiateItemPrefab(Vector3 position)
    {
        GameObject newItem = Instantiate(CoinPref, position, Quaternion.identity);
        Vector3 jumpTarget = position + new Vector3(0f, 0.5f, 0f);
        float jumpHeight = 0.5f;
        float jumpDuration = 0.5f;

        newItem.transform.DOLocalJump(jumpTarget, jumpHeight, 1, jumpDuration);

        return newItem;
    }

    private Vector3 GetSpawnPosition(int index)
    {
        Vector3 startPosition = new Vector3(-9f, 0.5f, 9f); 
        float xOffset = 0.5f; 
        float zOffset = 0.8f; 
        float yOffset = 0.4f; 
        int maxHorizontalCount = 5; 
        int maxForwardCount = 15; 

        Vector3 position = startPosition;
        int horizontalIndex = index % maxHorizontalCount; // Yatay konum indeksi
        int forwardIndex = index / maxHorizontalCount; // Dikey konum indeksi
        position.x += horizontalIndex * xOffset;
        position.z -= (forwardIndex % maxForwardCount) * zOffset;

        position.y += (forwardIndex / maxForwardCount) * yOffset;

        return position;
    }

    private void ResetValue(float value)
    {
        value = 0;
    }

}
