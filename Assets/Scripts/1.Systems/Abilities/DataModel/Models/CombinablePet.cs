using System;

// Contractable

[Serializable]
public class CombinablePet : Pet , ICombinable
{
    public CombinablePet(Pet pet)
    {
        Name = pet.Name;
        StatusAbility = pet.StatusAbility;
        Attributes = pet.Attributes;
    }
    public bool Assemble(ICombinable[] combinables)
    {
        throw new NotImplementedException();
    }

    public bool IsCombinableWith(string id)
    {
        throw new NotImplementedException();
    }

    public void Combine()
    {
        throw new NotImplementedException();
    }
}