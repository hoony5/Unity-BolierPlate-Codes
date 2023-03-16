using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDurationEffect", menuName = "ScriptableObject/Battle/Effects")]
public class DurationEffect : SkillEffect, IDurationEffect
{
    public string Name => _rawName;

    public void SetDamage(float damage)
    {
    }

    public void SetEffectIndex(int index)
    {
    }

    [ToDo(@"After creating Formula System, then Update Apply Damage Method")]
    public void UpdateEffect(Character character, Character other, float deltaTime)
    {
        
    }

    protected override void ApplyToAttacker(Character attacker)
    {
        
    }

    protected override void ApplyToTarget(Character target)
    {
        
    }
}