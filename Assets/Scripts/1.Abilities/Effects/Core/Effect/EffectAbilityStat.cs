using System;
using UnityEngine;

[System.Serializable]
public class EffectAbilityStat
{
    [field: SerializeField] public string RawName { get; set; }
    [field: SerializeField] public string DisplayName { get; set; }
    [field: SerializeField] public float Value { get; set; }
    [field: SerializeField] public float AddedValue { get; set; }
    [field: SerializeField] public float MultipliedValue { get; set; }
    [field: SerializeField] public float PreviousValue { get; set; }
    [field: SerializeField] public int Min { get; set; }
    [field: SerializeField] public int Max { get; set; }
    [field: SerializeField] public int Index { get; set; }
    [field: SerializeField] public ApplyTargetType ApplyTargetType {get; set;}
    [field: SerializeField] public CalculationType CalculationType {get; set;}
    [field: SerializeField] public DataUnitType DataUnitType {get; set;}
    
    public EffectAbilityStat(string statRawName, float value, int min, int max, string calculationType, string applyTargetType, string dataUnitType)
    {
        RawName = statRawName;
        Value = value;
        Min = min;
        Max = max;
        
        CalculationType = Enum.TryParse(calculationType, out CalculationType result) ? result : CalculationType.None;
        ApplyTargetType = Enum.TryParse(applyTargetType, out ApplyTargetType result2) ? result2 : ApplyTargetType.None;
        DataUnitType = Enum.TryParse(dataUnitType, out DataUnitType result3) ? result3 : DataUnitType.None;
    }   
    
    public void AddValue(float value)
    {
        Value += value;
        if (Value > Max) Value = Max;
        if (Value < Min) Value = Min;
    }
    public void InitValue()
    {
        Value = 0;
    }
    
}