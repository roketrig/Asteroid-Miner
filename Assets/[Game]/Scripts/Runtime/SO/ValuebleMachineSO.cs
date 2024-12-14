using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Machine Data", menuName = "Create Machine Data")]
public class ValuebleMachineSO : ScriptableObject
{
    public string machineName;
    public List<string> acceptableItems;
    public ProductItemSO productItem;

}
