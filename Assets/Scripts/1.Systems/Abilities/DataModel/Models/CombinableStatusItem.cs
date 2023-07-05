[System.Serializable]
public class CombinableStatusItem : StatusItem , ICombinable
{
    public CombinableStatusItem(StatusItem item)
    {
        Name = item.Name;
        Attributes = item.Attributes;
        StatusAbility = item.StatusAbility;
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