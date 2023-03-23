using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using ExcelDataReader;
using UnityEngine;

public class ExcelCsvReader : MonoBehaviour
{
    private const int FixedCapcity = 32;
    private const string KorEnCoding = "ks_c_5601-1987";
    public List<string> columnDataDictKeys = new List<string>(FixedCapcity);
    public List<ColumnData> columnDataDictValues = new List<ColumnData>(FixedCapcity);
    
    public List<string> rowDataDictKeys = new List<string>(FixedCapcity);
    public List<RowData> rowDataDictValues = new List<RowData>(FixedCapcity);

    public Object excelFile;
    public string path;
    public string sheetName;

    // TODO :: nested Cell Tracking, add ignore cell marker
    public void LoadDocument(string filePath, string sheetName)
    {
        ExcelReaderConfiguration config = new ExcelReaderConfiguration();
        config.FallbackEncoding = Encoding.GetEncoding(KorEnCoding);

        List<ColumnData> columnDataList = new List<ColumnData>();
        Dictionary<string, RowData> rowDataDict = new Dictionary<string, RowData>();

        using (FileStream streamer = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(streamer, config))
            {
                // Load data into dataset
                DataSet dataset = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true,
                    }
                });

                // Load column data into list
                DataTable table = dataset.Tables[sheetName];
                for (int index = 0; index < table.Columns.Count; index++)
                {
                    DataColumn column = table.Columns[index];
                    ColumnData columnData = new ColumnData();
                    columnData.Header = column.ColumnName;
                    
                    if (index != 0 && column.ColumnName.Contains($"Column{index}"))
                    {
                        columnData.Header = $"_{columnDataList[index - 1].Header}";
                    }
                    
                    columnData.Values = new List<string>();
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        string value = table.Rows[i][column].ToString();
                        if(columnData.Header.Contains("_") && string.IsNullOrEmpty(value))
                            columnData.Values.Add(columnDataList[index - 1].Values[i]);
                        else
                            columnData.Values.Add(value);
                    }

                    columnDataList.Add(columnData);
                }

                // Load row data into dictionary
                for (int index = 0; index < table.Rows.Count; index++)
                {
                    DataRow row = table.Rows[index];
                    RowData rowData = new RowData();
                    rowData.FirstColumnValue = row[0].ToString();
                    rowData.columnHeaders = new List<string>();
                    rowData.columnValues = new List<string>();
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        string header = table.Columns[i].ColumnName;
                        if (i != 0 && header.Contains($"Column{i}"))
                        {
                            header = $"_{rowData.columnHeaders[i - 1]}";
                        }
                        rowData.columnHeaders.Add(header);
                        string value = row[i].ToString();
                        if(header.Contains("_") && string.IsNullOrEmpty(value))
                            rowData.columnValues.Add(rowData.columnValues[i - 1]);
                        else
                            rowData.columnValues.Add(value);
                    }

                    rowDataDict.Add(rowData.FirstColumnValue, rowData);
                }
            }
        }

        // Copy list and dictionary data to public variables
        columnDataDictKeys.Clear();
        columnDataDictValues.Clear();
        rowDataDictKeys.Clear();
        rowDataDictValues.Clear();

        foreach (ColumnData columnData in columnDataList)
        {
            columnDataDictKeys.Add(columnData.Header);
            columnDataDictValues.Add(columnData);
        }

        foreach (KeyValuePair<string, RowData> entry in rowDataDict)
        {
            rowDataDictKeys.Add(entry.Key);
            rowDataDictValues.Add(entry.Value);
        }
    }
}
