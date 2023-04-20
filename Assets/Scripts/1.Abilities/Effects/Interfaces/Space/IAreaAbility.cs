using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// if character is on the Area, the effect will be applied. 
/// </summary>
public interface IAreaAbility : IAbility
{
    float Range { get; set; }
    int DetectObjectOnValidateArea(Character character, int areaMask, ref Collider[] result);
}