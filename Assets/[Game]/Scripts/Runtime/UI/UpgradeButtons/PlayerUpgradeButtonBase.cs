using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using ProjectBase.Core;
using UnityEngine.UI;
using ProjectBase.Utilities;
public class PlayerUpgradeButtonBase : MonoBehaviour
{
    protected PlayerUpgradeSO UpgradeData;

    [Header("ICON")]
    [SerializeField] private Image _upgradeIcon;

    [Header("SLIDER")]
    [SerializeField] private Slider _levelSlider;

    [Header("BUTTON")]
    [SerializeField] private Button _upgradeButton;

    [Header("TEXTS")]
    [SerializeField] private TextMeshProUGUI _titleTextMesh;
    [SerializeField] private TextMeshProUGUI _priceTextMesh;
    [SerializeField] private TextMeshProUGUI _levelBarTextMesh;

    protected virtual void OnEnable()
    {
        if (Managers.Instance == null)
        {
            return;
        }
        _upgradeButton.onClick.AddListener(UpgradeButtonOnClick);
    }

    protected virtual void OnDisable()
    {
        if (Managers.Instance == null)
        {
            return;
        }
        _upgradeButton.onClick.RemoveListener(UpgradeButtonOnClick);
    }

    protected virtual void Start()
    {
        _upgradeIcon.sprite = UpgradeData.UpgradeIcon;
        _levelSlider.maxValue = UpgradeData.MaxLevel;
        UpdateLevelBar();
        SetPrice();
    }

    protected virtual void UpgradeButtonOnClick()
    {
        if (GameManager.Instance.PlayerData.CurrencyData[ExchangeType.Coin] >= UpgradeData.UpgradeDataHolder.MoneyPrice)
        {
            GameManager.Instance.PlayerData.DecreaseCurrencyData(ExchangeType.Coin, UpgradeData.UpgradeDataHolder.MoneyPrice);
            UpgradeData.UpgradeMoney();
            SetPrice();
            CheckPriceState();
            UpdateLevelBar();
        }
    }
    //ok
    protected virtual void UpdateLevelBar()
    {
        if (UpgradeData.UpgradeDataHolder.Level == UpgradeData.MaxLevel)
        {
            _levelBarTextMesh.SetText("MAX");
            _upgradeButton.gameObject.SetActive(false);
        }
        else
        {
            _levelBarTextMesh.SetText(UpgradeData.UpgradeDataHolder.Level + "/" + UpgradeData.MaxLevel);
        }
        _levelSlider.value = UpgradeData.UpgradeDataHolder.Level;
    }

    protected virtual void SetPrice()
    {
        _priceTextMesh.SetText(ProjectBaseUtilities.ScoreShow(UpgradeData.UpgradeDataHolder.MoneyPrice));
    }
    protected virtual void CheckPriceState()
    {
        if (GameManager.Instance.PlayerData.CurrencyData[ExchangeType.Coin]>= UpgradeData.UpgradeDataHolder.MoneyPrice)
        {
            _upgradeButton.interactable = true;
        }
        else
        {
            _upgradeButton.interactable = false;
        }
    }
}
