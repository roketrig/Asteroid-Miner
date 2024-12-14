using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectBase.Utilities;

public class PlayerUpgradeManager : Singleton<PlayerUpgradeManager>
{
    public PlayerUpgradeSO movementSpeedData;
    public PlayerUpgradeSO capacityData;
    public PlayerUpgradeSO IncomeData;

    private void Awake()
    {
        movementSpeedData.InitData();
        capacityData.InitData();
        IncomeData.InitData();
    }
}
