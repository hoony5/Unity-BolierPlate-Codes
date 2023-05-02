using UnityEngine;

public class Character : ModuleController, ICharacter
{
    [field: SerializeField] public int Level { get; private set; }
    [field: SerializeField] public Status StatusAbility { get; private set; }
    [field: SerializeField] public CharacterBehaviour Behaviour { get; private set; }
    
    [ToDo("Need Ability Name, type by type")]
    public float GetDamage(AbilityType abilityType)
    {
        throw new System.NotImplementedException();
    }

    public float GetLifeValue(ChangedValueStatusType changedValueStatusType)
    {
        throw new System.NotImplementedException();
    }

    public float GetArmor(AbilityType abilityType)
    {
        throw new System.NotImplementedException();
    }
}