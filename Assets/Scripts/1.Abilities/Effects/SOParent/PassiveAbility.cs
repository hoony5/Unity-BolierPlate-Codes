﻿using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new NormalEffect", menuName = "ScriptableObject/Battle/NormalEffect", order = 0)]
public class PassiveAbility : EffectItem, ITeamAbility
{
    [field:SerializeField] public bool BuffOrDebuff { get; set; }
    [field:SerializeField] public List<EffectAbility> EffectAbilities { get; set; }
    public void UpdateAbility(Character[] characters)
    {
        throw new System.NotImplementedException();
    }
}