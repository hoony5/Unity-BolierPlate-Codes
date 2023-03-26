﻿using UnityEngine;

public class NormalEffect : EffectInfoBase
{
    [SerializeField] private CalculationType calculationType;
    [SerializeField] private ApplyTargetType applyTargetType;
    [SerializeField] private float adustment;
    
    public float Adustment => adustment;
    public CalculationType CalculationType => calculationType;
    public ApplyTargetType ApplyTargetType => applyTargetType;
}