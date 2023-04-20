using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AreaMotivatedAbility : Effect, IAreaMotivatedAbility
{
    [field:SerializeField] public bool IsStackable { get; set; }
    [field:SerializeField] public int StackCount { get; set; }
    [field:SerializeField] public float Range { get; set; }
    [field:SerializeField] public float Motivation { get; set; }
    [field:SerializeField] public float Chance { get; set; }
    [field:SerializeField] public int ApplyTargetCount { get; set; }
    [field:SerializeField] public ApplyTargetType ApplyTargetType { get; set; }
    [field:SerializeField] public List<EffectAbilityInfo> EffectAbilities { get; set; }
    [field:SerializeField] public string Description { get; set; }

    public int DetectObjectOnValidateArea(Character character, int areaMask, ref Collider[] result)
    {
        Transform transform = character.transform;
        Vector3 position = transform.position;
        Vector3 detectorSize = new Vector3(Range, position.y * 0.5f, Range);
        return Physics.OverlapBoxNonAlloc(position,  detectorSize, result, Quaternion.identity, areaMask);
    }

    public bool IsMotivatedWhenGreater(float motivation)
    {
        return motivation > Motivation;
    }
    public bool IsMotivatedWhenLess(float motivation)
    {
        return motivation < Motivation;
    }
    public bool IsMotivatedWhenApproximately(float motivation, float threshold = 0.01f)
    {
        return motivation - Motivation < threshold;
    }
    public bool HitTheChance(float tryChance)
    {
        return  tryChance <= Chance;
    }
}