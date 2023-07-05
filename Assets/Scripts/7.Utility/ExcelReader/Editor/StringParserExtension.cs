using System;
using System.Globalization;

public static class StringParserExtension
{
    public static float ToFloat(this string value, IFormatProvider formatProvider = null)
    {
        return float.TryParse(value,NumberStyles.Float, formatProvider, out float fNumber) ? fNumber : 0;
    }
    public static int ToInt(this string value, IFormatProvider formatProvider = null)
    {
        return int.TryParse(value,NumberStyles.Integer, formatProvider, out int iNumber) ? iNumber : 0;
    }
    public static bool ToBool(this string value)
    {
        return value is "TRUE" or "True" or "true" or "1";
    }
    public static object ToEnum(this string value, Type type)
    {
        return Enum.TryParse(type, value, out object result) ? result : null;
    }
}