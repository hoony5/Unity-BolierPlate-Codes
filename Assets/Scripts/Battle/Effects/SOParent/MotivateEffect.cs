﻿using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new MotivateEffect", menuName = "ScriptableObject/Battle/MotivateEffect", order = 0)]
public class MotivateEffect : EffectInfoBase, IMotivatedEffect
{
    [field:SerializeField] public List<EffectAbility> EffectAbilities { get; set; }
    public bool TryCheckMotivation(bool isMotivated)
    {
        throw new System.NotImplementedException();
    }
}