using System;
using UnityEngine;

[System.Serializable,Obsolete]
public class NormalEffectReference : EffectReferenceInfo
{
    [SerializeField] private CalculationType calculationType;
    [SerializeField] private ApplyTargetType applyTargetType;
    [SerializeField] private float adjustment;
    
    public float Adustment => adjustment;
    public CalculationType CalculationType => calculationType;
    public ApplyTargetType ApplyTargetType => applyTargetType;
}