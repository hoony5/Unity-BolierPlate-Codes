public static class NumberEx
{
    /// <summary>
    /// 0 ~ 10_000 for converting to 0 ~ 1f 
    /// </summary>
    public static int RandomRangeByInt()
    {
        return UnityEngine.Random.Range(0, 10_001);
    }
    public static float IntToFloat(this int value, int digit = 2)
    {
        digit = System.Math.Clamp(digit, 0, 4);

        return digit switch
        {
            0 => value,
            1 => value * 0.1f,
            2 => value * 0.01f,
            3 => value * 0.001f,
            4 => value * 0.0001f,
        };
    }
    
    public static int FloatToInt(this float value, int digit = 2)
    {
        return digit switch
        {
            0 => (int)value,
            1 => (int)(value * 10),
            2 => (int)(value * 100),
            3 => (int)(value * 1000),
            4 => (int)(value * 10000)
        };
    }
}