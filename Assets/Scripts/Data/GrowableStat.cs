
using UnityEngine;

[System.Serializable]
public class GrowableStat
{
    [field: SerializeField] public string RawName { get; set; }
    [field: SerializeField] public AnimationCurve GrowthTrendGraph { get; set; }        
}