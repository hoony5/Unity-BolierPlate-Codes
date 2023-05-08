using UnityEngine;

[System.Serializable]
public class Character : ModuleController, ICharacter
{
    [field: SerializeField] public int Level { get; private set; }
    [field: SerializeField] public CharacterType CharacterType { get; private set; }
    [field: SerializeField] public Status StatusAbility { get; private set; }
    [field: SerializeField] public CharacterBehaviour Behaviour { get; private set; }

}