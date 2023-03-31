using UnityEngine;
using UnityEngine.VFX;

[System.Serializable]
public abstract class EffectReferenceInfo : ScriptableObject
{
    [SerializeField] private string effectName;
    [SerializeField] private string description;
    [SerializeField] private Sprite icon;
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private VisualEffect vfx;
    [SerializeField] private AudioClip audioClip;
    
    public string EffectName => effectName;
    public string Description => description;
    public Sprite Icon => icon;
    public ParticleSystem ParticleSystem => particleSystem;
    public VisualEffect VFX => vfx;
    public AudioClip AudioClip => audioClip;
}