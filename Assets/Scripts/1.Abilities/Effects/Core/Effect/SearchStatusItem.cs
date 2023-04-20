[System.Serializable]
public class SearchStatusItem
{
    public StatusItemInfo statusItemInfo;
    public DataUnit  searchUnit;
    public bool isMeetCondition;

    public SearchStatusItem(StatusItemInfo statusItemInfo, DataUnit searchUnit)
    {
        this.statusItemInfo = statusItemInfo;
        this.searchUnit = searchUnit;
    }
}