using UnityEngine;

/// <summary>
/// For Battle Status
/// </summary>
[System.Serializable]
public class StatusItemInfo 
{
    [field: SerializeField] public string RawName { get; set; }
    [field: SerializeField] public string DisplayName { get; set; }
    [field: SerializeField] public float Value { get; private set; }
    [field: SerializeField] public int Min { get; private set; }
    [field: SerializeField] public int Max { get; private set; }
    [field: SerializeField] public int Index { get; set; }

    public static readonly StatusItemInfo Empty = new StatusItemInfo {
        RawName = "Empty",
        DisplayName = "Empty",
        Value = 0,
        Index = -1,
        Min = 0,
        Max = 9999
    };
    
    public void MultiplyValue(float value)
    {
        Value *= value;
    }
    public void AddValue(float value)
    {
        Value += value;
    }
    public void SetValue(float value)
    {
        Value = value;
    }
    public void SetRange(int min, int max)
    {
        Min = min;
        Max = max;
    }
}