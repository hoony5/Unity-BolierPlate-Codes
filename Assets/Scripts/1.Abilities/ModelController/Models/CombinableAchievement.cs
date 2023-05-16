using System;

[Serializable]
public class CombinableAchievement : Achievement , ICombinable
{
    public CombinableAchievement(Achievement achievement)
    {
        Name = achievement.Name;
        StatusAbility = achievement.StatusAbility;
        Attributes = achievement.Attributes;
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