[System.Serializable]
public class AbilityResourceInfo
{
    public string index;
    public string path;
    public string description;
    // Row Data - adapted from RowData.cs
    public string firstColumnValue; // key
    public string[] columnHeaders;
    public string[] columnValues;
}