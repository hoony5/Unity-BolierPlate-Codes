using UnityEngine;

public class NormalEffectReference : EffectReferenceInfo
{
    [SerializeField] private CalculationType calculationType;
    [SerializeField] private ApplyTargetType applyTargetType;
    [SerializeField] private float adustment;
    
    public float Adustment => adustment;
    public CalculationType CalculationType => calculationType;
    public ApplyTargetType ApplyTargetType => applyTargetType;
}