using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using ExcelDataReader;

namespace Utility.ExcelReader
{
    public static class ExcelCsvReader
    {
        // No ANSI
        private const string KorEnCoding = "ks_c_5601-1987";
        
        public static ExcelSheetInfo LoadDocument(string filePath, string sheetName)
        {
            int TypeIndex = 0;
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
                        columnData.Type = table.Rows[TypeIndex].ItemArray[index].ToString();

                        if (column.ColumnName.Contains($"Column{index}")) continue;

                        columnData.Values = new List<string>();
                        for (int i = TypeIndex + 1; i < table.Rows.Count; i++)
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

                    // Load row data into dictionary, index 0 is not header
                    for (int index = 0; index < table.Rows.Count; index++)
                    {
                        object[] row = table.Rows[index].ItemArray;
                        RowData rowData = new RowData();
                        rowData.FirstColumnValue = row[0].ToString();

                        if (string.IsNullOrEmpty(rowData.FirstColumnValue)) continue;

                        rowData.Headers = new List<string>();
                        rowData.Types = new List<string>();
                        rowData.Values = new List<string>();
                        
                        for (int i = 0; i < table.Columns.Count; i++)
                        {
                            string header = table.Columns[i].ColumnName;
                            if (header.Contains($"Column{i}")) continue;

                            rowData.Headers.Add(header);
                            string value = row[i].ToString();

                            if (value == "True")
                                value = "true";
                            if (value == "False")
                                value = "false";

                            rowData.Values.Add(value);
                            rowData.Types.Add(table.Rows[TypeIndex].ItemArray[i].ToString());
                        }

                        rowDataDict.Add(rowData.FirstColumnValue, rowData);
                    }
                }
            }

            ExcelSheetInfo excelSheetInfo = new ExcelSheetInfo();
            excelSheetInfo.TypeName = sheetName;

            foreach (ColumnData columnData in columnDataList)
            {
                excelSheetInfo.ColumnDataDict.Add(columnData.Header, columnData);
            }

            //excelSheetInfo.RowDataDict.TryAdd(this.sheetName, new SerializedDictionary<string, RowData>(rowDataDict));
            foreach (RowData rowData in rowDataDict.Values)
            {
                _ = excelSheetInfo.RowDataDict.TryAdd(rowData.FirstColumnValue, rowData);    
            }

            return excelSheetInfo;
        }
    }
}