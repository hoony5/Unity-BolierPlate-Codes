using System;

[System.Serializable]
public class EffectAbilityStat
{
    [UnityEngine.SerializeField] private StatusItemInfo _statusItemInfo;
    [UnityEngine.SerializeField] private ApplyTargetType applyTargetType;
    [UnityEngine.SerializeField] private CalculationType calculationType;
    
    public string StatRawName => _statusItemInfo.RawName; 
    public float Value => _statusItemInfo.Value;
    public float Min => _statusItemInfo.Min;
    public float Max => _statusItemInfo.Max;
    public CalculationType CalculationType => calculationType;
    public ApplyTargetType ApplyTargetType => applyTargetType;
    
    public EffectAbilityStat(string statRawName, float value, int min, int max, string calculationType, string applyTargetType)
    {
        _statusItemInfo = new StatusItemInfo
        {
            RawName = statRawName,
            Value = value,
            Min = min,
            Max = max
        };
        
        this.calculationType = Enum.TryParse(calculationType, out CalculationType result) ? result : CalculationType.None;
        this.applyTargetType = Enum.TryParse(applyTargetType, out ApplyTargetType result2) ? result2 : ApplyTargetType.None;
    }
    
    public void AddValue(float value)
    {
        _statusItemInfo.Value += value;
        if (_statusItemInfo.Value > Max) _statusItemInfo.Value = Max;
        if (_statusItemInfo.Value < Min) _statusItemInfo.Value = Min;
    }
    public void InitValue()
    {
        _statusItemInfo.Value = 0;
    }
    
}