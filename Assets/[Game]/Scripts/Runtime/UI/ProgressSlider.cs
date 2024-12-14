using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using ProjectBase.Core;

public class ProgressSlider : MonoBehaviour
{
    public TextMeshPro percentageText;
    public Slider progressSlider;
    public TextMeshPro upgradeText;
    private int upgradePrice = 250;
    private float currentProgress = 0;
    private float maxProgress = 10;
    private double coinAmount;

    void Awake()
    {
        coinAmount = GameManager.Instance.PlayerData.CurrencyData[ExchangeType.Coin];
    }

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        int stable = (int)Math.Round((currentProgress / maxProgress) * 100);
        percentageText.text = $"{stable}%";
        upgradeText.text = upgradePrice.ToString();
    }

    public void IncreaseProgress(float amount)
    {
        currentProgress += amount;
        if (currentProgress > maxProgress)
        {
            currentProgress = maxProgress;
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        float progressPercentage = (currentProgress / maxProgress) * 100;
        percentageText.text = $"{progressPercentage:F1}%";
        progressSlider.value = currentProgress / maxProgress;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && coinAmount >= upgradePrice)
        {
            GameManager.Instance.PlayerData.DecreaseCurrencyData(ExchangeType.Coin, upgradePrice);
            IncreaseProgress(1);
            UpgradeCost();
            upgradeText.text = upgradePrice.ToString();
        }
    }

    private void UpgradeCost()
    {
        upgradePrice += 250;
    }
}
