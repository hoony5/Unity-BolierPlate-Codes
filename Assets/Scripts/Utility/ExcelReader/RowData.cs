using System.Collections.Generic;

[System.Serializable]
public class RowData
{
    public string FirstColumnValue; // key
    public List<string> columnHeaders;
    public List<string> columnValues;
}