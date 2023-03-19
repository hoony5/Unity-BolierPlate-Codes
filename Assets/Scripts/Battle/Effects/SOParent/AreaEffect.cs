﻿using UnityEngine;

[CreateAssetMenu(fileName = "new AreaEffect", menuName = "ScriptableObject/Battle/AreaEffect", order = 0)]
public class AreaEffect : EffectInfoBase, IAreaEffect
{
    [field:SerializeField] public float Range { get; set; }
    public bool TryCheckArea(Character character, int areaMask)
    {
        throw new System.NotImplementedException();
    }
}