using Newtonsoft.Json;

public static class CopyEx
{
    public static T DeepCopy<T>(this T obj) where T : class
    {
        if (obj is null) return default;
        return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj));
    }
    
    public static T DeepCopy<T>(this T obj, JsonSerializerSettings settings) where T : class
    {
        if (obj is null) return default;
        return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj, settings));
    }
}
