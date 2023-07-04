using System;

namespace Utility.ExcelReader
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class SetExcelDataAttribute : Attribute
    {
        public Type Type { get; set; }
        public string Key { get; set; }

        public SetExcelDataAttribute(Type type, string key)
        {
            Type = type;
            Key = key;
        }
    }
}
