using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AreaAbility : Effect, IAreaAbility
{
    [field:SerializeField] public bool IsStackable { get; set; }
    [field:SerializeField] public int StackCount { get; set; }
    [field:SerializeField] public int MaxStackCount { get; set; }
    [field:SerializeField] public float Range { get; set; }
    [field:SerializeField] public int ApplyTargetCount { get; set; }
    [field:SerializeField] public ApplyTargetType ApplyTargetType { get; set; }
    [field:SerializeField] public float Chance { get; set; }
    [field:SerializeField] public List<EffectAbilityInfo> EffectAbilities { get; set; }
    [field:SerializeField] public string Description { get; set; }
    public bool HitTheChance(float tryChance)
    {
        return  tryChance <= Chance;
    }
    public int DetectObjectOnValidateArea(Character character, int areaMask, ref Collider[] result)
    {
        Transform transform = character.Transform;
        Vector3 position = transform.position;
        Vector3 detectorSize = new Vector3(Range, position.y * 0.5f, Range);
        return Physics.OverlapBoxNonAlloc(position,  detectorSize, result, Quaternion.identity, areaMask);
    }
    public void AddStackCount()
    {
        if (!IsStackable) return;
        StackCount++;
        if(StackCount > MaxStackCount) StackCount = MaxStackCount;
    }
    public void SubtractStackCount()
    {
        if (!IsStackable) return;
        StackCount--;
        if (StackCount <= 0) StackCount = 1;
    }
    public void ResetStackCount()
    {
        if (!IsStackable) return;
        StackCount = 1;
    }
}