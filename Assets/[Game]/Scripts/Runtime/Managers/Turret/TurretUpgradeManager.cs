using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUpgradeManager : MonoBehaviour
{
    public TurretUpgradeSO turretFireRateData;
    public TurretUpgradeSO turretRangeData;
    public TurretUpgradeSO turretDamageData;

    private void Awake()
    {
        turretFireRateData.InitData();
        turretRangeData.InitData();
        turretDamageData.InitData();
    }
}
