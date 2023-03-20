using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new DurationAimedEffect", menuName = "ScriptableObject/Battle/Combined/Time/DurationAimedEffect", order = 0)]
public class DurationAimedEffect : EffectInfoBase, IDurationAimedEffect
{
    [field:SerializeField] public float Duration { get; set; }
    [field:SerializeField] public string SearchState { get; set; }
    [field:SerializeField] public string SearchTag { get; set; }
    [field:SerializeField] public List<StatusItemInfo> SearchStats { get; set; }
    
    public bool TryCheckTime(float currentDuration)
    {
        throw new System.NotImplementedException();
    }

    public bool TryUpdateEffect(Character other, string abilityName, float threshold, float value)
    {
        throw new System.NotImplementedException();
    }

    public bool TryCheckState(Character character, string stateName)
    {
        throw new System.NotImplementedException();
    }
}