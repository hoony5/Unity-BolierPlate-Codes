using System.Collections.Generic;

[System.Serializable]
public class SearchStatusInfo
{
    public string effectName;
    public List<SearchStatusItem> items;
    
    public SearchStatusInfo(string effectName, List<SearchStatusItem> items)
    {
        this.effectName = effectName;
        this.items = items;
    }
}
