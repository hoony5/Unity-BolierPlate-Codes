using System;
using System.Reflection;
using UnityEngine;

namespace Utility.ExcelReader.Editor
{
    public static class ExcelDataParser
    {
        private static BindingFlags _bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        public static T FromData<T>(RowData rowData)
        {
            T instance = Activator.CreateInstance<T>();
            Type type = instance.GetType();
        
            FieldInfo[] fields = type.GetFields(_bindingFlags);
            if(fields.Length > 0) instance.SetFields(fields, rowData);
        
            PropertyInfo[] properties = type.GetProperties(_bindingFlags);
            if(properties.Length > 0) instance.SetProperties(properties, rowData);
        
            return instance;
        }

        private static bool IsExceptionOfColumnIndex(ColumnIndexAttribute columnAttribute, RowData rowData)
        {
            if(columnAttribute.Index < 0)
            {
                Debug.LogError("Index is less than 0");
                return true;
            }
            if(columnAttribute.Index >= rowData.ColumnHeaders.Count)
            {
                Debug.LogError("Index is greater than columnHeaders.Count");
                return true;
            }

            return false;
        }
        private static void SetFields<T>(this T instance, FieldInfo[] fields, RowData rowData)
        {
            foreach (FieldInfo field in fields)
            {
                if (!Attribute.IsDefined(field, typeof(ColumnIndexAttribute))) continue;
            
                ColumnIndexAttribute columnAttribute = field.GetCustomAttribute<ColumnIndexAttribute>(true);
            
                if(IsExceptionOfColumnIndex(columnAttribute, rowData)) continue;
            
                field.SetValue(instance, rowData.ColumnValues[columnAttribute.Index]);
            }
        }
        private static void SetProperties<T>(this T instance, PropertyInfo[] properties, RowData rowData)
        {
            foreach (PropertyInfo field in properties)
            {
                if (!Attribute.IsDefined(field, typeof(ColumnIndexAttribute))) continue;
            
                ColumnIndexAttribute columnAttribute = field.GetCustomAttribute<ColumnIndexAttribute>(true);
                
                if(IsExceptionOfColumnIndex(columnAttribute, rowData)) continue;
            
                field.SetValue(instance, rowData.ColumnValues[columnAttribute.Index]);
            }
        }
    
    }
}
