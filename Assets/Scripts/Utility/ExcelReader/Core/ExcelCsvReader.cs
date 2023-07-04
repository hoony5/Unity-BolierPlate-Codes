using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using ExcelDataReader;
using UnityEngine;

namespace Utility.ExcelReader
{
    public class ExcelCsvReader : MonoBehaviour
    {
        private const string KorEnCoding = "ks_c_5601-1987";
        // Test
        public Object excelFile;
        public string path;
        public string sheetName;

        public void SaveDatabase(string filePath, string sheetName)
        {
            LoadDocument(filePath, sheetName);
        }
        public ExcelSheetInfo LoadDocument(string filePath, string sheetName)
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

                        if (column.ColumnName.Contains($"Column{index}")) continue;

                        columnData.Values = new List<string>();
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            string value = table.Rows[i][column].ToString();
                            if (string.IsNullOrEmpty(value)) continue;

                            if (value == "True")
                                value = "true";
                            if (value == "False")
                                value = "false";

                            columnData.Values.Add(value);
                        }

                        columnDataList.Add(columnData);
                    }

                    // Load row data into dictionary
                    for (int index = 0; index < table.Rows.Count; index++)
                    {
                        object[] row = table.Rows[index].ItemArray;
                        RowData rowData = new RowData();
                        rowData.FirstColumnValue = row[0].ToString();

                        if (string.IsNullOrEmpty(rowData.FirstColumnValue)) continue;

                        rowData.ColumnHeaders = new List<string>();
                        rowData.ColumnValues = new List<string>();
                        for (int i = 0; i < table.Columns.Count; i++)
                        {
                            string header = table.Columns[i].ColumnName;
                            if (header.Contains($"Column{i}")) continue;

                            rowData.ColumnHeaders.Add(header);
                            string value = row[i].ToString();

                            if (value == "True")
                                value = "true";
                            if (value == "False")
                                value = "false";

                            rowData.ColumnValues.Add(value);
                        }

                        rowDataDict.Add(rowData.FirstColumnValue, rowData);
                    }
                }
            }

            ExcelSheetInfo excelSheetInfo = new ExcelSheetInfo();
            // Copy list and dictionary data to public variables
            excelSheetInfo.ColumnDataDict.Clear();
            excelSheetInfo.RowDataDict.Clear();

            foreach (ColumnData columnData in columnDataList)
            {
                excelSheetInfo.ColumnDataDict.Add(columnData.Header, columnData);
            }

            foreach (KeyValuePair<string, RowData> entry in rowDataDict)
            {
                excelSheetInfo.RowDataDict.Add(entry.Key, entry.Value);
            }

            return excelSheetInfo;
        }
    }
}