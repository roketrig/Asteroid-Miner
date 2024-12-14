using ProjectBase.Core;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectorUpgradeRangeSystem : MonoBehaviour
{
    public Slider slider;
    public float decreaseSpeed = 1f;
    public CollectorUpgradeSO upgradeSO;
    public Collector collector;
    public TextMeshPro upgradeCostText;

    private Coroutine decreaseCoroutine;

    private void Start()
    {
        if (upgradeSO != null)
        {
            upgradeSO.InitData();
            collector.hookRange = upgradeSO.UpgradeDataHolder.Value;
            UpdateUpgradeCostText(); 
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (upgradeSO.UpgradeDataHolder.Level >= upgradeSO.MaxLevel)
            {
                slider.value = 0;
                UpdateUpgradeCostText(); 
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

                        collector.hookRange = upgradeSO.UpgradeDataHolder.Value;
                        UpdateUpgradeCostText();
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
            upgradeCostText.text = $"Hand Range Upgrade: {currentUpgradeCost}";
        }
    }
}
