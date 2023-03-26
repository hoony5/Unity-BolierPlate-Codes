using UnityEngine;

[CreateAssetMenu(fileName = "new DurationEffect", menuName = "ScriptableObject/Battle/DurationEffect", order = 0)]
public class DurationEffect : EffectInfoBase, IDurationEffect
{
    [field:SerializeField] public float Duration { get; set; }
    public bool TryCheckTime(float currentDuration)
    {
        throw new System.NotImplementedException();
    }
}