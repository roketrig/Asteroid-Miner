using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

[CreateAssetMenu(fileName = "Player Upgrade Data", menuName = "Create Player Upgrade Data")]
public class PlayerUpgradeSO : ScriptableObject
{
    public string UpgradeName;
    [HideInInspector]public UpgradeDataList UpgradeDataHolder;

    [Header("CONFIG")]
    public int MaxLevel;

    [Header("DEFAULT VALUES")]

    [SerializeField] private float _defaultValue;
    [SerializeField] private float _defaultMoneyPrice;
    //[SerializeField] private int _defaultDiamondPrice;

    [Space(20)]

    [Header("INCREASE AMOUNTS")]

    [SerializeField] private float _valueIncreaseAmount;
    [SerializeField] private float _moneyPriceIncreaseMultiplier;
    //[SerializeField] private float _diamondPriceIncreaseMultiplier;

    [Space(20)]

    [InlineEditor(InlineEditorModes.LargePreview)]

    public Sprite UpgradeIcon;

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
            //UpgradeDataHolder.DiamondPrice = _defaultMoneyPrice;
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
   // public void UpgradeDiamond()

}
[Serializable]
public class UpgradeDataList
{
    public float Value;
    public float MoneyPrice;
    public int DiamondPrice;
    public int Level;
}