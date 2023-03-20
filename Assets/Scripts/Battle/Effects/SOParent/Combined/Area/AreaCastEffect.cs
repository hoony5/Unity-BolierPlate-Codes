using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new AreaCastEffect", menuName = "ScriptableObject/Battle/Combined/Area/AreaCastEffect", order = 0)]
public class AreaCastEffect : EffectInfoBase, IAreaCastEffect
{
    [field:SerializeField] public float Range { get; set; }
    [field:SerializeField] public float Threshold { get; set; }
   
    public bool TryCheckArea(Character character, int areaMask)
    {
        throw new System.NotImplementedException();
    }

    public bool TryCheckThreshold(float threshold)
    {
        throw new System.NotImplementedException();
    }
}