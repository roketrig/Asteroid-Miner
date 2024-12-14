using ProjectBase.Utilities;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorUpgradeManager : Singleton<CollectorUpgradeManager>
{
    public CollectorUpgradeSO collectorSpeedData;
    public CollectorUpgradeSO collectorRangeData;

    private void Awake()
    {
        collectorSpeedData.InitData();
        collectorRangeData.InitData();
    }
}
