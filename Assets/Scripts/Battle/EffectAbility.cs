[System.Serializable]
public class EffectAbility
{
    [UnityEngine.SerializeField] private string statRawName;
    [UnityEngine.SerializeField] private ApplyTargetType applyTargetType;
    [UnityEngine.SerializeField] private CalculationType calculationType;
    [UnityEngine.SerializeField] private float value;
    
    public string StatRawName => statRawName; 
    public float Value => value;
    public CalculationType CalculationType => calculationType;
    public ApplyTargetType ApplyTargetType => applyTargetType;
}