using UnityEngine;

public abstract class SkillInfoBase : ScriptableObject
{
    [SerializeField] protected string rawName;
    public string RawName => rawName;
        
    [SerializeField] protected float baseValue;
    public float BaseValue => baseValue;
        
    [SerializeField] protected string description;
    public string Description => description;

    [SerializeField] protected SkillEffect _effect;
}