using UnityEngine;

public abstract class Attack : ScriptableObject
{
    [SerializeField] private string rawName;
    public string RawName => rawName;
        
    [SerializeField] private float baseValue;
    public float BaseValue => baseValue;
        
    [SerializeField] private string description;
    public string Description => description;
}