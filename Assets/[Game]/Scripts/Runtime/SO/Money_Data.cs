using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using ProjectBase.Core;

[CreateAssetMenu(fileName = "Money Data", menuName = "Create Money Data")]

public class Money_Data : ScriptableObject
{

    [SerializeField]
    double coinAmount = GameManager.Instance.PlayerData.CurrencyData[ExchangeType.Coin];
    public double Coin
    {
        get { return coinAmount; }
        set { coinAmount = value; }
    }
}
