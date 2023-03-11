using UnityEngine;

/// <summary>
/// For Battle Status
/// </summary>
[System.Serializable]
public class StatusItemInfo 
{
    [field: SerializeField] public string Name { get; set; }
    [field: SerializeField] public float Value { get; set; }
    [field: SerializeField] public int Index { get; set; }
    
    public static readonly StatusItemInfo Empty = new StatusItemInfo {
        Name = "Empty",
        Value = 0,
        Index = -1
    };
}