using UnityEngine;

[CreateAssetMenu(fileName = "new AreaDurationEffect", menuName = "ScriptableObject/Battle/Combined/Area/AreaDurationEffect", order = 0)]
public class AreaDurationEffect : EffectInfoBase, IAreaEffect, IDurationEffect
{
    [field:SerializeField] public float Range { get; set; }
    [field:SerializeField] public float Duration { get; set; }
    
    public bool TryCheckTime(float currentDuration)
    {
        throw new System.NotImplementedException();
    }
    public bool TryCheckArea(Character character, int areaMask)
    {
        throw new System.NotImplementedException();
    }
}