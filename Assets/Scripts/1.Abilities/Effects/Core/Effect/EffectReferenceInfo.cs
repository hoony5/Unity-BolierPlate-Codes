using UnityEngine;
using UnityEngine.VFX;

[System.Serializable]
public class EffectReferenceInfo : ScriptableObject
{
    [field: SerializeField] public string EffectName{get;set;}
    [field: SerializeField] public string Description{get;set;}
    [field: SerializeField] public Sprite Icon{get;set;}
    [field: SerializeField] public ParticleSystem ParticleSystem{get;set;}
    [field: SerializeField] public VisualEffect VFX{get;set;}
}