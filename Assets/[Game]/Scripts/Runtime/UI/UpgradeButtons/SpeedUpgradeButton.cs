using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpgradeButton : PlayerUpgradeButtonBase
{
    private void Awake()
    {
        UpgradeData = PlayerUpgradeManager.Instance.movementSpeedData;
    }
}
