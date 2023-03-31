public interface ICharacter
{
    // Battle
    float GetDamage(AbilityType abilityType);
    float GetLifeValue(LifeValueType lifeValueType);
    float GetArmor(AbilityType abilityType);
}