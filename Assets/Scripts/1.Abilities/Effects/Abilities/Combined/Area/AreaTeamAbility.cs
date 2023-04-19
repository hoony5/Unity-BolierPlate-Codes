﻿using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AreaTeamAbility : Effect, IAreaTeamAbility
{
    [field:SerializeField] public bool IsStackable { get; set; }
    [field:SerializeField] public int StackCount { get; set; }
    [field:SerializeField] public float Range { get; set; }
    [field:SerializeField] public bool BuffOrDebuff { get; set; }
    [field:SerializeField] public bool IsPassive { get; set; }
    [field:SerializeField] public List<EffectAbilityInfo> EffectAbilities { get; set; }
    [field:SerializeField] public string Description { get; set; }

    public bool DetectObjectOnValidateArea(Character character, int areaMask, ref Collider[] result)
    {
        Transform transform = character.transform;
        Vector3 position = transform.position;
        Vector3 detectorSize = new Vector3(Range, position.y * 0.5f, Range);
        return Physics.OverlapBoxNonAlloc(position,  detectorSize, result, Quaternion.identity, areaMask) > 0;
    }
    public void UpdateAbility(Character[] characters)
    {
        throw new System.NotImplementedException();
    }
}