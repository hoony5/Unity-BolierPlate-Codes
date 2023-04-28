using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// this class is used to store all the status names and their index
/// </summary>
[System.Serializable, CreateAssetMenu(fileName = "newCostInfo", menuName = "ScriptableObject/Effect/CostInfo")]
public class AllCostInfos : ScriptableObject
{
    [SerializeField] private ExpenseAbilityInfo[] costs;
    private Dictionary<string, ExpenseAbilityStat[]> _costIndexMap;
}