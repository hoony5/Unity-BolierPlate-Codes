[System.Serializable]
public class AbilityDataInfo
{
    // Row Data - adapted from RowData.cs
    public string firstColumnValue; // key
    public string[] columnHeaders;
    public string[] columnValues;

    public AbilityDataInfo(){ }
    public AbilityDataInfo(string  firstColumnValue, string[] columnHeaders, string[] columnValues)
    {
        this.firstColumnValue = firstColumnValue;
        this.columnHeaders = columnHeaders;
        this.columnValues = columnValues;
    }
}