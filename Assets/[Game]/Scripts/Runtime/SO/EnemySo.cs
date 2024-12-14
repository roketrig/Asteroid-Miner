using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Create Enemy Data")]
public class EnemySo : ScriptableObject
{
    public string enemyName;
    public int enemyHealth;
    public int enemyDamage;
    public int coinLoot;

}
