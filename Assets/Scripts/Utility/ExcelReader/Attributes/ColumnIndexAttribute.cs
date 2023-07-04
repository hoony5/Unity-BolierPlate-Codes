using System;

namespace Utility.ExcelReader
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ColumnIndexAttribute : Attribute
    {
        public int Index { get; set; }

        public ColumnIndexAttribute(int index)
        {
            Index = index;
        }
    }
}