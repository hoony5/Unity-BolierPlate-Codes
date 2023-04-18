using System.Collections.Generic;

public interface IAbility
{
    List<EffectAbilityInfo> EffectAbilities { get; set; }
    string Description { get;}
    bool IsStackable { get; set; }
    int StackCount { get; set; }
}
