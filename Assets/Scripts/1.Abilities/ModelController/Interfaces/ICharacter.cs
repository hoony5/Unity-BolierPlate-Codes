public interface ICharacter
{
    // Battle
    float GetDamage(AbilityType abilityType);
    float GetLifeValue(ChangedValueStatusType changedValueStatusType);
    float GetArmor(AbilityType abilityType);
}