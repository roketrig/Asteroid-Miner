using ProjectBase.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // TextMeshPro kullanımı için gerekli

public class AsteroidUpgradeSpeedSystem : MonoBehaviour
{
    public Slider slider;
    public float decreaseSpeed = 1f;
    public AsteroidUpgradeSO upgradeSO;
    public Hook Hook;
    public TextMeshPro upgradeCostText; // Upgrade bedeli için TMP text

    private Coroutine decreaseCoroutine;

    private void Start()
    {
        if (upgradeSO != null)
        {
            upgradeSO.InitData();
            Hook.hookSpeed = upgradeSO.UpgradeDataHolder.Value;
            UpdateUpgradeCostText(); // Başlangıçta upgrade bedelini göster
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Max level kontrolü
            if (upgradeSO.UpgradeDataHolder.Level >= upgradeSO.MaxLevel)
            {
                // Max level'a ulaşıldıysa slider'ı sıfırla ve durdur
                slider.value = 0;
                UpdateUpgradeCostText(); // Max level mesajını güncelle
                return;
            }

            if (decreaseCoroutine != null)
            {
                StopCoroutine(decreaseCoroutine);
            }

            slider.value += Time.deltaTime * 50;

            if (slider.value >= 100)
            {
                slider.value = 0;

                if (upgradeSO == null || GameManager.Instance == null || GameManager.Instance.PlayerData == null)
                {
                    return;
                }

                if (CheckIfEnoughMoney(upgradeSO.UpgradeDataHolder.MoneyPrice))
                {
                    if (upgradeSO.UpgradeDataHolder.Level < upgradeSO.MaxLevel)
                    {
                        GameManager.Instance.PlayerData.DecreaseCurrencyData(ExchangeType.Coin, upgradeSO.UpgradeDataHolder.MoneyPrice);
                        upgradeSO.UpgradeMoney();

                        Hook.hookSpeed = upgradeSO.UpgradeDataHolder.Value;
                        UpdateUpgradeCostText(); // Upgrade sonrası bedeli güncelle
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            decreaseCoroutine = StartCoroutine(DecreaseSlider());
        }
    }

    IEnumerator DecreaseSlider()
    {
        while (slider.value > 0)
        {
            slider.value -= decreaseSpeed;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private bool CheckIfEnoughMoney(float requiredMoney)
    {
        var CoinAmount = GameManager.Instance.PlayerData.CurrencyData[ExchangeType.Coin];
        return CoinAmount >= requiredMoney;
    }

    private void UpdateUpgradeCostText()
    {
        var currentUpgradeCost = upgradeSO.UpgradeDataHolder.MoneyPrice;
        if (upgradeSO.UpgradeDataHolder.Level >= upgradeSO.MaxLevel)
        {
            upgradeCostText.text = "Max Level!";
        }
        else
        {
            upgradeCostText.text = $"Hand Speed Upgrade: {currentUpgradeCost}";
        }
    }
}
