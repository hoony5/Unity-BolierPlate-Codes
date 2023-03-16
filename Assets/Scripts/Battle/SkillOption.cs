using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "new SkillOption", menuName = "ScriptableObject/Battle/SkillOption", order = 0)]
public class SkillOption : ScriptableObject
{
    [SerializeField] private string rawName;
    public string RawName => rawName;
    
    [SerializeField] private float multiplier;
    public float Multiplier => multiplier;
    
    [SerializeField] private float range;
    public float Range => range;
    
    [SerializeField] private float coolTime;
    public float CoolTime => coolTime;
    
    [SerializeField] private float duration;
    public float Duration => duration;
    
    [SerializeField] private float cost;
    public float Cost => cost;
    
    [SerializeField] private Sprite icon;
    public Sprite Icon => icon;
    
    [SerializeField] private ParticleSystem particleSystem;
    public ParticleSystem ParticleSystem => particleSystem;
    
    [SerializeField] private VisualEffect vfx;
    public VisualEffect VFX => vfx;
    
    [SerializeField] private AudioClip audioClip;
    public AudioClip AudioClip => audioClip;
}