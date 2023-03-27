using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new CastAimedEffect",
    menuName = "ScriptableObject/Battle/Combined/Time/CastAimedEffect", order = 0)]
public class CastAimedAbility : EffectInfoBase, ICastAimedAbility
{
    [field:SerializeField] public float Threshold { get; set; }
    [field:SerializeField] public string SearchState { get; set; }
    [field:SerializeField] public string SearchTag { get; set; }
    [field:SerializeField] public List<StatusItemInfo> SearchStats { get; set; }
    
    public bool TryCheckState(Character character, string stateName)
    {
        throw new System.NotImplementedException();
    }
    public bool TryUpdateEffect(Character character, string abilityName, float threshold, float value)
    {
        throw new System.NotImplementedException();
    }
    public bool TryCheckThreshold(float threshold)
    {
        throw new System.NotImplementedException();
    }

}