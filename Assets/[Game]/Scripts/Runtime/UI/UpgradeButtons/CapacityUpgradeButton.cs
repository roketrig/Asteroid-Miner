using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapacityUpgradeButton : PlayerUpgradeButtonBase
{
    private void Awake()
    {
        UpgradeData = PlayerUpgradeManager.Instance.capacityData;
    }
}
