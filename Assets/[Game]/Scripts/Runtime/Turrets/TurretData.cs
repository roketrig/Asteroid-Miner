using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret Data", menuName = "Create Turret Data")]
public class TurretData : ScriptableObject
{
    public bool isTurretUnlocked = false;  // Turret'ın kilidi açık mı
}
