using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidUpgradeManager : MonoBehaviour
{
    public AsteroidUpgradeSO asteroidSpeedData;
    public AsteroidUpgradeSO asteroidRangeData;

    private void Awake()
    {
        asteroidSpeedData.InitData();
        asteroidRangeData.InitData();
    }
}
