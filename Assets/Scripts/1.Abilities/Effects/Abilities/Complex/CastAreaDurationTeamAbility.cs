﻿using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CastAreaDurationTeamAbility : Effect, ICastAreaDurationTeamAbility
{
    [field:SerializeField] public bool IsStackable { get; set; }
    [field:SerializeField] public int StackCount { get; set; }
    [field:SerializeField] public bool BuffOrDebuff { get; set; }
    [field:SerializeField] public float Duration { get; set; }
    [field:SerializeField] public float Range { get; set; }
    [field:SerializeField] public float Threshold { get; set; }
    [field:SerializeField] public List<EffectAbilityInfo> EffectAbilities { get; set; }
    [field:SerializeField] public string Description { get; set; }
    
    public bool DetectObjectOnValidateArea(Character character, int areaMask, ref Collider[] result)
    {
        Transform transform = character.transform;
        Vector3 position = transform.position;
        Vector3 detectorSize = new Vector3(Range, position.y * 0.5f, Range);
        return Physics.OverlapBoxNonAlloc(position,  detectorSize, result, Quaternion.identity, areaMask) > 0;
    }
    public bool HasThresholdPassed(float threshold)
    {
        return threshold >= Threshold;
    }
    public bool HasTimePassed(float currentDuration)
    {
        return currentDuration >= Duration;
    }
    public void UpdateAbility(Character[] characters)
    {
        throw new System.NotImplementedException();
    }
}