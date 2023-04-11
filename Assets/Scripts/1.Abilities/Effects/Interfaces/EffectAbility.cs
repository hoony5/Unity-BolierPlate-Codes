using System;

[System.Serializable]
public class EffectAbility
{
    [UnityEngine.SerializeField] private string statRawName;
    [UnityEngine.SerializeField] private ApplyTargetType applyTargetType;
    [UnityEngine.SerializeField] private CalculationType calculationType;
    [UnityEngine.SerializeField] private float value;
    [UnityEngine.SerializeField] private float min;
    [UnityEngine.SerializeField] private float max;
    
    public string StatRawName => statRawName; 
    public float Value => value;
    public float Min => min;
    public float Max => max;
    public CalculationType CalculationType => calculationType;
    public ApplyTargetType ApplyTargetType => applyTargetType;
    
    public EffectAbility(string statRawName, float value, float min, float max, string calculationType, string applyTargetType)
    {
        this.statRawName = statRawName;
        this.value = value;
        this.min = min;
        this.max = max;
        this.calculationType = Enum.TryParse(calculationType, out CalculationType result) ? result : CalculationType.None;
        this.applyTargetType = Enum.TryParse(applyTargetType, out ApplyTargetType result2) ? result2 : ApplyTargetType.None;
    }
    
    public void AddValue(float value)
    {
        this.value += value;
        if (this.value > max) this.value = max;
        if (this.value < min) this.value = min;
    }
    public void InitValue()
    {
        value = 0;
    }
    
}