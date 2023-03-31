using UnityEngine;

[CreateAssetMenu(fileName = "new DurationEffect", menuName = "ScriptableObject/Battle/DurationEffect", order = 0)]
public class DurationAbility : EffectReferenceInfo, IDurationAbility
{
    [field:SerializeField] public float Duration { get; set; }
    public bool TryCheckTime(float currentDuration)
    {
        throw new System.NotImplementedException();
    }
}