using System.Collections.Generic;

public interface IAbility
{
    List<EffectAbilityInfo> EffectAbilities { get; set; }
    float Chance { get;}
    string Description { get;}
    bool IsStackable { get; set; }
    int StackCount { get; set; }
    bool HitTheChance(float tryChance);
}
