using UnityEngine;

[CreateAssetMenu(fileName = "new CastEffect", menuName = "ScriptableObject/Battle/CastEffect", order = 0)]
public class CastAbility : EffectItem, IThresholdAbility
{
    public float Threshold { get; set; }
    public bool TryCheckThreshold(float threshold)
    {
        throw new System.NotImplementedException();
    }
}