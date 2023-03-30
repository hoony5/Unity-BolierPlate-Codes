using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new AreaAimedEffect", menuName = "ScriptableObject/Battle/Combined/Area/AreaAimedEffect", order = 0)]
public class AreaTeamAbility : EffectItem, IAreaTeamAbility
{
    [field:SerializeField] public float Range { get; set; }
    [field:SerializeField] public bool BuffOrDebuff { get; set; }
    [field:SerializeField] public bool IsPassive { get; set; }
    [field:SerializeField] public List<StatusItemInfo> SearchStats { get; set; }
    [field:SerializeField] public List<EffectAbility> EffectAbilities { get; set; }

    public bool TryCheckArea(Character character, int areaMask)
    {
        throw new System.NotImplementedException();
    }
    public void UpdateAbility(Character[] characters)
    {
        throw new System.NotImplementedException();
    }
}