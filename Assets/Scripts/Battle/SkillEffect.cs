using UnityEngine;
using UnityEngine.Serialization;

public abstract class SkillEffect : ScriptableObject
{
    [SerializeField] protected string _rawName;
    [SerializeField] protected SkillAbility skillAbility; 
    [SerializeField] protected SkillOption skillOption; 
    protected abstract void ApplyToAttacker(Character attacker);
    protected abstract void ApplyToTarget(Character target);
}