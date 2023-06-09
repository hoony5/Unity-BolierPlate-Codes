using System.Collections.Generic;

public interface IAbility
{
    bool IsStackable { get; set; }
    ApplyTargetType ApplyTargetType { get; set; }
    bool IsHitChance(float tryChance);
    int ApplyTargetCount { get; set; }
    int StackCount { get; set; }
    int MaxStackCount { get; set; }
    float Chance { get; set;}
    string Description { get; set;}
    List<EffectAbilityInfo> EffectAbilities { get; set; }
    void AddStackCount();
    void SubtractStackCount();
    void ResetStackCount();
}
