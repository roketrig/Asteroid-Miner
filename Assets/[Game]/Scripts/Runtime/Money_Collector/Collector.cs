using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [Header("Hook")]
    public GameObject hookPrefab;
    public float hookSpeed;
    public float hookRange;
    [SerializeField] private Transform mainobject;
    private bool isHookEmpty;
    [Title("General")]
    public GameObject coinPref;
    public Transform coinspawnPoint;
    public WaveSpawner waveSpawner;

    [Title("Animations")]
    public ParticleSystem rocketParticle; 
    public Transform particleSpawnPoint; 
    public float takeOffHeight = 5f; 
    public float takeOffDuration = 1f; 
    public float landingDuration = 1f; 
    public Vector3 particleRotation = new Vector3(0f, 180f, 0f); 

    private void Start()
    {
        waveSpawner.OnWaveCompleted += OnWaveCompleted;
    }

    private void OnDestroy()
    {
        waveSpawner.OnWaveCompleted -= OnWaveCompleted; 
    }

    private void OnWaveCompleted()
    {
        StartCoroutine(CollectAllBags());
    }

    IEnumerator CollectAllBags()
    {
        ParticleSystem instantiatedParticle = Instantiate(rocketParticle, particleSpawnPoint.position, Quaternion.Euler(particleRotation));
        instantiatedParticle.transform.SetParent(transform); 
        instantiatedParticle.gameObject.SetActive(true);
        instantiatedParticle.Play();

        yield return transform.DOMove(transform.position + Vector3.up * takeOffHeight, takeOffDuration).WaitForCompletion();
        GameObject[] bags = GameObject.FindGameObjectsWithTag("Collect");
        List<Vector3> bagPositions = new List<Vector3>();
        List<GameObject> collectedBags = new List<GameObject>();
        foreach (GameObject bag in bags)
        {
            Loot_bag lootBagComponent = bag.GetComponent<Loot_bag>();
            if (lootBagComponent != null)
            {
                bagPositions.Add(bag.transform.position);
                collectedBags.Add(bag);
            }
        }

        if (collectedBags.Count == 0)
        {
            yield return transform.DOMove(mainobject.position, landingDuration).WaitForCompletion();
            instantiatedParticle.Stop();
            Destroy(instantiatedParticle.gameObject); 
            isHookEmpty = true;
            yield break;
        }
        foreach (GameObject bag in collectedBags)
        {
            Vector3 targetPosition = bag.transform.position;
            float distance = Vector3.Distance(transform.position, targetPosition);
            float duration = distance / hookSpeed;

            yield return transform.DOMove(targetPosition, duration).WaitForCompletion();

            bag.transform.SetParent(transform);
        }
        Vector3 mainObjectPosition = mainobject.position;
        float maxDistance = 0f;
        foreach (GameObject bag in collectedBags)
        {
            float distance = Vector3.Distance(mainObjectPosition, bag.transform.position);
            if (distance > maxDistance)
            {
                maxDistance = distance;
            }
        }

        float returnDuration = maxDistance / hookSpeed;

        yield return transform.DOMove(mainObjectPosition, returnDuration).WaitForCompletion();
        foreach (GameObject bag in collectedBags)
        {
            Destroy(bag.gameObject);
           // bag.SetActive(false);
            Loot_bag lootBagComponent = bag.GetComponent<Loot_bag>();
            if (lootBagComponent != null)
            {
                SpawnCoins(lootBagComponent.lootData.coinLoot);
            }
        }
        yield return transform.DOMove(mainObjectPosition, landingDuration).WaitForCompletion();
        instantiatedParticle.Stop();
        Destroy(instantiatedParticle.gameObject);

        isHookEmpty = true;
    }

    void SpawnCoins(int coinCount)
    {
        int rows = 2; // 2 satır
        int columns = 3; // 3 sütun
        float spacing = 1f; // Coinler arasındaki mesafe

        int coinsToSpawn = Mathf.Min(coinCount, rows * columns); // Maksimum 2x3'lük alanda coinler spawn olacak

        for (int i = 0; i < coinsToSpawn; i++)
        {
            // Satır ve sütun hesaplama
            int row = i / columns;
            int column = i % columns;

            // Pozisyon hesaplama (coinspawnPoint'ten başlayarak -z ekseninde ilerleyerek yerleştir)
            Vector3 spawnPosition = coinspawnPoint.position + new Vector3(0, 0, row * spacing) + new Vector3(0, 0, column * spacing);

            // x ekseninde 90 derece rotasyonla Coin'i oluştur
            Quaternion coinRotation = Quaternion.Euler(0, 0, 0); // x ekseninde 90 derece döndürülmüş rotasyon
            Instantiate(coinPref, spawnPosition, coinRotation);
        }
    }




}
