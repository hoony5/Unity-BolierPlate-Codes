using System.Collections.Generic;

[System.Serializable]
public class SearchStatusItem
{
    public string effectName;
    public List<StatusItemInfo>  statusItemInfos;

    public SearchStatusItem(string effectName, List<StatusItemInfo> statusItemInfos)
    {
        this.effectName = effectName;
        this.statusItemInfos = statusItemInfos;
    }
}