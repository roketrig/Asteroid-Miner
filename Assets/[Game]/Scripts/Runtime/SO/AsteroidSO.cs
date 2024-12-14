using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Asteroid Data",menuName ="Create Asteroid Data")]
public class AsteroidSO : ScriptableObject
{
    public GameObject gemPrefab;
    public int productSize;
    public float pullTime;
    public AsteroidType AsteroidType;

}
