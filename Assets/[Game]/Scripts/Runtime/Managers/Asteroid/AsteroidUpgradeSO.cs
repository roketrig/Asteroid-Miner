using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Asteroid Upgrade Data", menuName = "Create Asteroid Upgrade Data")]
public class AsteroidUpgradeSO : ScriptableObject
{
    public string UpgradeName;
    [HideInInspector] public UpgradeDataList UpgradeDataHolder;

    [Header("CONFIG")]
    public int MaxLevel;

    [Header("DEFAULT VALUES")]
    [SerializeField] private float _defaultValue;
    [SerializeField] private float _defaultMoneyPrice;

    [Header("INCREASE AMOUNTS")]
    [SerializeField] private float _valueIncreaseAmount;
    [SerializeField] private float _moneyPriceIncreaseMultiplier;

    public void InitData()
    {
        if (PlayerPrefs.HasKey(UpgradeName))
        {
            string savedData = PlayerPrefs.GetString(UpgradeName);
            UpgradeDataHolder = JsonUtility.FromJson<UpgradeDataList>(savedData);
        }
        else
        {
            UpgradeDataHolder.Value = _defaultValue;
            UpgradeDataHolder.MoneyPrice = _defaultMoneyPrice;
            UpgradeDataHolder.Level = 0;
        }
        SaveData();
        Debug.Log(UpgradeName + "Initialize");
    }

    private void SaveData()
    {
        string data = JsonUtility.ToJson(UpgradeDataHolder);
        PlayerPrefs.SetString(UpgradeName, data);
    }

    public void UpgradeMoney()
    {
        UpgradeDataHolder.Value += _valueIncreaseAmount; 
        UpgradeDataHolder.MoneyPrice *= _moneyPriceIncreaseMultiplier;
        UpgradeDataHolder.Level++;
        SaveData();
    }
    [Serializable]
    public class AsteroidDataList
    {
        public float Speed;
        public float Range;
    }
}

