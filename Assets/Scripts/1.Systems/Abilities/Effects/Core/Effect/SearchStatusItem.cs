using UnityEngine;

[System.Serializable]
public class SearchStatusItem
{
    [field:SerializeField] public StatusItemInfo StatusItemInfo { get; set; }
    [field:SerializeField] public DataUnitType  SearchUnitType { get; set; }
    public bool isMeetCondition;

    public SearchStatusItem(StatusItemInfo statusItemInfo, DataUnitType searchUnitType)
    {
        this.StatusItemInfo = statusItemInfo;
        this.SearchUnitType = searchUnitType;
    }
}