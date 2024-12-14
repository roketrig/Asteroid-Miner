using DG.Tweening;
using ProjectBase.Core;
using TMPro;
using UnityEngine;

public class UnlockScrForTurret : MonoBehaviour
{
    public float NumToUnlock;
    private double CoinAmount;
    public TextMeshPro TxtAmtUnlock;
    public GameObject ItemToUnlock;
    public GameObject ObjectToDestroy;
    private bool isopen = false;
    private Vector3 originalPosition;
    public GameObject TempObject; // Yeni atadığınız obje

    public TurretUnlockDataSO turretUnlockData;

    private void Start()
    {
        TxtAmtUnlock.text = NumToUnlock.ToString();
        originalPosition = ItemToUnlock.transform.localPosition;

        turretUnlockData.InitData();

        if (turretUnlockData.isTurretUnlocked)
        {
            UnlockTurret();
            TempObject.SetActive(false); 
        }
        else
        {
            TempObject.SetActive(true); 
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
                UnlockTurret();
                Destroy(ObjectToDestroy);
            }
        }
    }

    private void UnlockTurret()
    {
        if (!isopen)
        {
            isopen = true;
            ItemToUnlock.SetActive(true);
            ItemToUnlock.transform.localPosition = originalPosition + new Vector3(0, -5f, 0);
            ItemToUnlock.transform.DOLocalMove(originalPosition, 0.5f).SetEase(Ease.OutBounce);

            turretUnlockData.UnlockTurret();
            TxtAmtUnlock.text = "";
        }
    }
}
