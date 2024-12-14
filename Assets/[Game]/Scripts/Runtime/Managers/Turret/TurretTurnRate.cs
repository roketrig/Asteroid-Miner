using ProjectBase.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TurretTurnRate : MonoBehaviour
{
    public Slider slider;
    public float decreaseSpeed = 1f;
    public TurretUpgradeSO upgradeSO;
    public TurretController Turret;

    private Coroutine decreaseCoroutine;

    private void Start()
    {
        if (upgradeSO != null)
        {
            upgradeSO.InitData();
            Turret.turnSpeed = upgradeSO.UpgradeDataHolder.Value;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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

                        Turret.turnSpeed = upgradeSO.UpgradeDataHolder.Value;
                    }
                    else
                    {
                        Debug.LogWarning($"Cannot upgrade {upgradeSO.UpgradeName}, already at max level!");
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
}
