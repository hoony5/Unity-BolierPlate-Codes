using UnityEngine;

/// <summary>
/// For Battle Status
/// </summary>
[System.Serializable]
public class StatusItemInfo 
{
    [field: SerializeField] public string RawName { get; set; }
    [field: SerializeField] public string DisplayName { get; set; }
    [field: SerializeField] public float Value { get; set; }
    [field: SerializeField] public int Index { get; set; }
    
    public static readonly StatusItemInfo Empty = new StatusItemInfo {
        RawName = "Empty",
        DisplayName = "Empty",
        Value = 0,
        Index = -1
    };
}