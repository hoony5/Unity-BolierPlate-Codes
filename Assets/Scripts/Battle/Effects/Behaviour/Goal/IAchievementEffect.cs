using System.Collections.Generic;
using UnityEngine.TextCore.Text;

/// <summary>
/// effect will be applied when the Something is achieved.
/// </summary>
public interface IAchievementEffect : IEffect
{
    List<EffectAbility> Effects { get; set; }
    void UpdateEffect(Character character, Character other, bool isAchieved);
}