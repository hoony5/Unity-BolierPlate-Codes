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
}