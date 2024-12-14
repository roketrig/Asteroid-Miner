using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using DG.Tweening;

public class AsteroidMachine : MonoBehaviour
{
    [SerializeField] private Transform _pointTransform;
    [SerializeField] private Vector3 _spawnPosition;
    [SerializeField] private List<Asteroid> asteroids = new List<Asteroid>();
    private bool isProductionActive;
    public int _spawnedGemsCount;
    [SerializeField] private Collider _collider;

    public void AddAstreoid(Asteroid asteroid)
    {
        if (!asteroids.Contains(asteroid))
        {
            asteroids.Add(asteroid);
            if (!isProductionActive)
            {
                StartProduction();
            }
        }
    }
    private async void StartProduction()
    {
        if (asteroids.Count > 0 && !isProductionActive)
        {
            isProductionActive = true;
            _collider.enabled = true;
            for (int i = 0; i < asteroids[0].AsteroidData.productSize; i++)
            {
                GameObject gem = Instantiate(asteroids[0].AsteroidData.gemPrefab, transform.position, Quaternion.Euler(0, 90, 0), _pointTransform);
                _spawnedGemsCount++;
                _spawnPosition = GetSpawnPosition();
                gem.transform.DOLocalJump(_spawnPosition, 3, 1, 0.5f);
                await UniTask.Delay(TimeSpan.FromSeconds(0.5f), ignoreTimeScale: false);
            }
            Destroy(asteroids[0]);
            asteroids.Remove(asteroids[0]);
            isProductionActive = false;
            _collider.enabled = false;
            StartProduction();
        }
    }

    private Vector3 GetSpawnPosition()
    {
        Vector3 position = new Vector3(-1.5f, 0.2f, 0.8f);
        int horizontalCounter = 0;
        int forwardCounter = 0;

        for (int i = 0; i < _spawnedGemsCount; i++)
        {
            if (horizontalCounter == 5)
            {
                position.z -= 0.8f;
                position.x = -1.5f;
                horizontalCounter = 0;
            }
            if (forwardCounter == 15)
            {
                position.z = 0.8f;
                position.y += 0.4f;
                forwardCounter = 0;
            }
            position.x += 0.5f;
            horizontalCounter++;
            forwardCounter++;
        }

        return position;
    }
}
