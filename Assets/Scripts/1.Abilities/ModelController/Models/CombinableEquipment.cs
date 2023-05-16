[System.Serializable]
public class CombinableEquipment : Equipment , ICombinable
{  
    public CombinableEquipment(Equipment equipment)
    {
        Name = equipment.Name;
        StatusAbility = equipment.StatusAbility;
        Attributes = equipment.Attributes;
    }
    public bool Assemble(ICombinable[] combinables)
    {
        throw new System.NotImplementedException();
    }

    public bool IsCombinableWith(string id)
    {
        throw new System.NotImplementedException();
    }

    public void Combine()
    {
        throw new System.NotImplementedException();
    }
}