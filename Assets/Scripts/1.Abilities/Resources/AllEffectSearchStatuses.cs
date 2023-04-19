
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName = "newEffectSearchStatuses", menuName = "ScriptableObject/Effect/EffectSearchStatuses")]
public class AllEffectSearchStatuses : ScriptableObject
{
    [field:SerializeField] private List<SearchStatusInfo> SearchStatuses { get; set; }
    private Dictionary<string, SearchStatusInfo> SearchStatusItemsMap { get; set; }

    private void OnEnable()
    {
        Reset();
    }

    public void Reset()
    {
        SearchStatusItemsMap.Clear();
        SearchStatusItemsMap = SearchStatuses.ToDictionary(key => key.effectName, value => value);
    }

    public void SetEffectInfomations(SearchStatusInfo[] searchStatuses)
    {
        SearchStatuses.AddRange(searchStatuses);
    }
}
