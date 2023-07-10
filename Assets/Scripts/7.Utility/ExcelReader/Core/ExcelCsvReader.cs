using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using AYellowpaper.SerializedCollections;
using ExcelDataReader;

namespace Utility.ExcelReader
{
    public static class ExcelCsvReader
    {
        private const string KorEnCoding = "ks_c_5601-1987";
        
        public static SerializedDictionary<string, ExcelSheetInfo> Read(string filePath)
        {
            const int typeIndex = 0;

            ExcelReaderConfiguration config = new ExcelReaderConfiguration
            {
                FallbackEncoding = Encoding.GetEncoding(KorEnCoding)
            };

            List<ColumnData> columnDataList = new List<ColumnData>();
            SerializedDictionary<string, RowData> rowDataDict = new SerializedDictionary<string, RowData>();

            using FileStream streamer = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            using IExcelDataReader reader = ExcelReaderFactory.CreateReader(streamer, config);

            DataSet dataset = reader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = _ => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true,
                }
            });

            SerializedDictionary<string, ExcelSheetInfo> result = new SerializedDictionary<string, ExcelSheetInfo>(dataset.Tables.Count); 

            foreach (DataTable table in dataset.Tables)
            {
                ProcessColumns(typeIndex, table, columnDataList);
                ProcessRows(typeIndex, table, rowDataDict);

                ExcelSheetInfo excelSheetInfo = new ExcelSheetInfo
                {
                    TypeName = table.TableName
                };

                foreach (ColumnData columnData in columnDataList)
                {
                    excelSheetInfo.ColumnDataDict[columnData.Header] =  columnData;
                }

                foreach (RowData rowData in rowDataDict.Values)
                {
                    excelSheetInfo.RowDataDict[rowData.FirstColumnValue] = rowData;
                }

                result[table.TableName] = excelSheetInfo;
            }

            return result;
        }

        private static void ProcessColumns(int typeIndex, DataTable table, List<ColumnData> columnDataList)
        {
            for (int index = 0; index < table.Columns.Count; index++)
            {
                DataColumn column = table.Columns[index];
                ColumnData columnData = new ColumnData
                {
                    Header = column.ColumnName,
                    Type = table.Rows[typeIndex].ItemArray[index].ToString()
                };

                if (column.ColumnName.Contains($"Column{index}")) continue;

                columnData.Values = new List<string>();
                for (var i = typeIndex + 1; i < table.Rows.Count; i++)
                {
                    string value = table.Rows[i][column].ToString();
                    if (string.IsNullOrEmpty(value)) continue;

                    value = NormalizeValue(value);

                    columnData.Values.Add(value);
                }

                columnDataList.Add(columnData);
            }
        }

        private static void ProcessRows(int typeIndex, DataTable table, Dictionary<string, RowData> rowDataDict)
        {
            for (int index = 0; index < table.Rows.Count; index++)
            {
                object[] row = table.Rows[index].ItemArray;
                RowData rowData = new RowData
                {
                    FirstColumnValue = row[0].ToString()
                };

                if (string.IsNullOrEmpty(rowData.FirstColumnValue)) continue;

                rowData.Headers = new List<string>();
                rowData.Types = new List<string>();
                rowData.Values = new List<string>();

                for (var i = 0; i < table.Columns.Count; i++)
                {
                    var header = table.Columns[i].ColumnName;
                    if (header.Contains($"Column{i}")) continue;

                    rowData.Headers.Add(header);
                    var value = row[i].ToString();

                    value = NormalizeValue(value);

                    rowData.Values.Add(value);
                    rowData.Types.Add(table.Rows[typeIndex].ItemArray[i].ToString());
                }

                rowDataDict.Add(rowData.FirstColumnValue, rowData);
            }
        }

        private static string NormalizeValue(string value)
        {
            if (value == "True")
                value = "true";
            if (value == "False")
                value = "false";

            return value;
        }
    }
}
