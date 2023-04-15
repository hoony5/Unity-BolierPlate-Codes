[System.Serializable]
public class AbilityResourceInfo
{
    public string typeName;
    public string path;
    public string description;
    // Row Data - adapted from RowData.cs
    public string firstColumnValue; // key
    public string[] columnHeaders;
    public string[] columnValues;

    public void SetAbilityData(string[] columnHeader, string[] columnValue)
    {
        columnHeaders = columnHeader;
        columnValues = columnValue;
        firstColumnValue = columnValue[0];
    }
}