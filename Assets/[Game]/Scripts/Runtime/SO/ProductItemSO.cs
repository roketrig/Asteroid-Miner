using UnityEngine;

[CreateAssetMenu(fileName = "Reqiurement Data", menuName = "Create Reqiurement Data")]
public class ProductItemSO : ScriptableObject
{
    public AsteroidType Key;
    public int TargetAmount;
}
