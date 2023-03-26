using UnityEngine;

public class Character : ModuleController
{
    [field: SerializeField] public int Level { get; }
    [field: SerializeField] public Status StatusAbility { get; }
    [field: SerializeField] public CharacterBehaviour Behaviour { get; }
}