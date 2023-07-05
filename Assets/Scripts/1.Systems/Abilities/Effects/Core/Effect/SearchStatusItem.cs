using UnityEngine.Serialization;

[System.Serializable]
public class SearchStatusItem
{
    public StatusItemInfo statusItemInfo;
    [FormerlySerializedAs("searchUnit")] public DataUnitType  searchUnitType;
    public bool isMeetCondition;

    public SearchStatusItem(StatusItemInfo statusItemInfo, DataUnitType searchUnitType)
    {
        this.statusItemInfo = statusItemInfo;
        this.searchUnitType = searchUnitType;
    }
}