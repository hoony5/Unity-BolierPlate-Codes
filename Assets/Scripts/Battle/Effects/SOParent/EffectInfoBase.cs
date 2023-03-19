using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[System.Serializable]
public abstract class EffectInfoBase : ScriptableObject
{
    [SerializeField] private string effectName;
    [SerializeField] private float baseValue;
    [SerializeField] private string description;
    [SerializeField] private float coolTime;
    [SerializeField] private float cost;
    [SerializeField] private Sprite icon;
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private VisualEffect vfx;
    [SerializeField] private AudioClip audioClip;
    
    public string EffectName => effectName;
    public float BaseValue => baseValue;
    public string Description => description;
    
    public float Cost => cost;
    public float CoolTime => coolTime;
    public Sprite Icon => icon;
    public ParticleSystem ParticleSystem => particleSystem;
    public VisualEffect VFX => vfx;
    public AudioClip AudioClip => audioClip;
}