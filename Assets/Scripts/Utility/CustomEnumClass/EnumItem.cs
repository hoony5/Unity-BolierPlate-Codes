[System.Serializable]
public class EnumItem<T>
{
    public EnumItem(string key, T value, int order)
    {
        Key = key;
        Value = value;
        Order = order;       
    }

    public string Key { get; set; }
    public T Value { get; set; }
    public int Order { get; set; }        
}