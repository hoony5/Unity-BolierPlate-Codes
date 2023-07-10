using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SearchStatusInfo
{
    [field:SerializeField] public string EffectName { get; set; }
    [field:SerializeField] public List<SearchStatusItem> Items { get; set; }
    
    public SearchStatusInfo(string effectName, List<SearchStatusItem> items)
    {
        this.EffectName = effectName;
        this.Items = items;
    }
}
