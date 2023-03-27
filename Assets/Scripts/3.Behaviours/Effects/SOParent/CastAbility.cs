using UnityEngine;

[CreateAssetMenu(fileName = "new CastEffect", menuName = "ScriptableObject/Battle/CastEffect", order = 0)]
public class CastAbility : EffectInfoBase, IThresholdAbility
{
    public float Threshold { get; set; }
    public bool TryCheckThreshold(float threshold)
    {
        throw new System.NotImplementedException();
    }
}