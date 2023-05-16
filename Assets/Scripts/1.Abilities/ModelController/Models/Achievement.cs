using UnityEngine;

[System.Serializable]
public class Achievement : ModuleController
{
    [field: SerializeField] public string Name { get; protected set; }
    [field: SerializeField] public Status StatusAbility { get; protected set; }
    [field: SerializeField] public AchievementAttributes Attributes { get; protected set; }
    public void SetName(string name)
    {
        Name = name;
    }
    public void SetAttributes(AchievementAttributes attributes)
    {
        Attributes = attributes;
    }

}