using DG.Tweening;
using ProjectBase.Core;
using TMPro;
using UnityEngine;

public class UnlockScrDrone : MonoBehaviour
{
    public float NumToUnlock;
    private double CoinAmount;
    public TextMeshPro TxtAmtUnlock;
    public GameObject ItemToUnlock;
    public GameObject ObjectToDestroy;
    private bool isopen = false;
    private Vector3 originalPosition;
    public Transform BossSpawnPoint; // Boss'un spawnlanacağı nokta
    public GameObject TempObject; // Yeni atadığınız obje
    public GameObject prefabToSpawn; // Sahneye çıkacak prefab

    public UnlockDataSO droneUnlockData;

    private void Start()
    {
        TxtAmtUnlock.text = NumToUnlock.ToString();
        originalPosition = ItemToUnlock.transform.localPosition;

        // Unlock verilerini başlat
        droneUnlockData.InitData();

        // Eğer drone zaten açılmışsa, hemen aç ve TempObject'i gizle
        if (droneUnlockData.isUnlocked)
        {
            UnlockDrone();
            TempObject.SetActive(false);
        }
        else
        {
            TempObject.SetActive(true); // Eğer drone açılmamışsa TempObject gösteriliyor
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isopen)
        {
            CoinAmount = GameManager.Instance.PlayerData.CurrencyData[ExchangeType.Coin];
            if (CoinAmount >= NumToUnlock)
            {
                GameManager.Instance.PlayerData.DecreaseCurrencyData(ExchangeType.Coin, NumToUnlock);
                UnlockDrone();
                Destroy(ObjectToDestroy); // Belirtilen objeyi yok et
            }
        }
    }

    private void UnlockDrone()
    {
        if (!isopen)
        {
            isopen = true;
            ItemToUnlock.SetActive(true); // Drone'u aç
            droneUnlockData.UnlockItem(); // Veriyi kaydet
            TxtAmtUnlock.text = ""; // Metni temizle

            // Prefab'ı BossSpawnPoint konumunda sahneye ekle
            SpawnPrefab();
        }
    }

    private void SpawnPrefab()
    {
        if (prefabToSpawn != null && BossSpawnPoint != null)
        {
            Instantiate(prefabToSpawn, BossSpawnPoint.position, BossSpawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("Prefab veya BossSpawnPoint referansı atanmadı!");
        }
    }
}
