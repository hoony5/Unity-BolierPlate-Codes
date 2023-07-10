using UnityEngine;

[System.Serializable]
public class Pet : ModuleController
{
    [field: SerializeField] public string Name { get;  set; }
    [field: SerializeField] public Transform Transform  { get; set; }
    [field: SerializeField] public PetAttributes Attributes { get;  set; }
    [field:SerializeField] public Status StatusAbility { get;  set; }

    public void SetName(string name)
    {
        Name = name;
    }

    public void SetAttributes(PetAttributes attributes)
    {
        Attributes = attributes;
    }
}   