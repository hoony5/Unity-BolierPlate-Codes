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
    private string[] _costIndexMapKeys;
    
    private void OnEnable()
    {
#if  UNITY_EDITOR
        // SerializedDictionary를 사용해야하나..
#else
        ConvertToDictionary();
#endif
    }

    private void ConvertToDictionary()
    {
        _costIndexMap = new Dictionary<string, ExpenseAbilityStat[]>(128);
        _costIndexMap = costs.ToDictionary(key => key.EffectName, value => value.ExpenseStats.ToArray());
    }
}