using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomeUpgradeButton : PlayerUpgradeButtonBase
{
    private void Awake()
    {
        UpgradeData = PlayerUpgradeManager.Instance.IncomeData;
    }
}
