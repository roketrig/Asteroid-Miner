using ProjectBase.Core;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurretFireRate : MonoBehaviour
{
    public Slider slider;
    public float decreaseSpeed = 1f;

    public TurretUpgradeSO upgradeSOTurnSpeed;  
    public TurretUpgradeSO upgradeSOFireRate;   
    public TurretUpgradeSO upgradeSORange;      

    public TextMeshProUGUI fireRateText;        
    public TextMeshProUGUI turnSpeedText;       
    public TextMeshProUGUI rangeText;           
    public TextMeshProUGUI totalCostText;       

    public TurretController Turret;

    private Coroutine decreaseCoroutine;

    private void Start()
    {
        InitAllUpgradeData();
        UpdateAllText();  
        UpdateTotalCost(); 
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !AreAllMaxLevel())  
        {
            if (decreaseCoroutine != null)
            {
                StopCoroutine(decreaseCoroutine);
            }

            slider.value += Time.deltaTime * 50;  

            if (slider.value >= 100)
            {
                slider.value = 0;

                if (CheckIfEnoughMoneyForAll())
                {
                    UpgradeAllStats();  
                    UpdateAllText();    
                    UpdateTotalCost();  
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

    private void InitAllUpgradeData()
    {
        if (upgradeSOFireRate != null)
        {
            upgradeSOFireRate.InitData();
            Turret.fireRate = upgradeSOFireRate.UpgradeDataHolder.Value;
        }

        if (upgradeSOTurnSpeed != null)
        {
            upgradeSOTurnSpeed.InitData();
            Turret.turnSpeed = upgradeSOTurnSpeed.UpgradeDataHolder.Value;
        }

        if (upgradeSORange != null)
        {
            upgradeSORange.InitData();
            Turret.range = upgradeSORange.UpgradeDataHolder.Value;
        }
    }

    private bool CheckIfEnoughMoneyForAll()
    {
        var CoinAmount = GameManager.Instance.PlayerData.CurrencyData[ExchangeType.Coin];
        float totalCost = upgradeSOFireRate.UpgradeDataHolder.MoneyPrice
                        + upgradeSOTurnSpeed.UpgradeDataHolder.MoneyPrice
                        + upgradeSORange.UpgradeDataHolder.MoneyPrice;

        return CoinAmount >= totalCost;
    }

    private void UpgradeAllStats()
    {
        float totalCost = upgradeSOFireRate.UpgradeDataHolder.MoneyPrice
                        + upgradeSOTurnSpeed.UpgradeDataHolder.MoneyPrice
                        + upgradeSORange.UpgradeDataHolder.MoneyPrice;

        GameManager.Instance.PlayerData.DecreaseCurrencyData(ExchangeType.Coin, totalCost);

        UpgradeStat(upgradeSOFireRate, ref Turret.fireRate, fireRateText);
        UpgradeStat(upgradeSOTurnSpeed, ref Turret.turnSpeed, turnSpeedText);
        UpgradeStat(upgradeSORange, ref Turret.range, rangeText);
    }

    private void UpgradeStat(TurretUpgradeSO upgradeSO, ref float turretStat, TextMeshProUGUI upgradeText)
    {
        if (upgradeSO.UpgradeDataHolder.Level < upgradeSO.MaxLevel)
        {
            upgradeSO.UpgradeMoney();
            turretStat = upgradeSO.UpgradeDataHolder.Value;
        }
        else
        {
            Debug.LogWarning($"Cannot upgrade {upgradeSO.UpgradeName}, already at max level!");
        }
        UpdateText(upgradeSO, upgradeText);  // Texti güncelle
    }

    private void UpdateAllText()
    {
        UpdateText(upgradeSOFireRate, fireRateText);
        UpdateText(upgradeSOTurnSpeed, turnSpeedText);
        UpdateText(upgradeSORange, rangeText);
    }

    private void UpdateText(TurretUpgradeSO upgradeSO, TextMeshProUGUI upgradeText)
    {
        if (upgradeSO.UpgradeDataHolder.Level < upgradeSO.MaxLevel)
        {
            upgradeText.text = $"{upgradeSO.UpgradeName} Lv{upgradeSO.UpgradeDataHolder.Level}";
        }
        else
        {
            upgradeText.text = $"{upgradeSO.UpgradeName} Max Level";
        }
    }

    private bool AreAllMaxLevel()
    {
        return upgradeSOFireRate.UpgradeDataHolder.Level >= upgradeSOFireRate.MaxLevel
            && upgradeSOTurnSpeed.UpgradeDataHolder.Level >= upgradeSOTurnSpeed.MaxLevel
            && upgradeSORange.UpgradeDataHolder.Level >= upgradeSORange.MaxLevel;
    }

    private void UpdateTotalCost()
    {
        if (AreAllMaxLevel())
        {
            totalCostText.text = "Max Level Reached";  
        }
        else
        {
            float totalCost = upgradeSOFireRate.UpgradeDataHolder.MoneyPrice
                            + upgradeSOTurnSpeed.UpgradeDataHolder.MoneyPrice
                            + upgradeSORange.UpgradeDataHolder.MoneyPrice;

            totalCostText.text = $"Total Upgrade Cost: {totalCost}";
        }
    }
}
