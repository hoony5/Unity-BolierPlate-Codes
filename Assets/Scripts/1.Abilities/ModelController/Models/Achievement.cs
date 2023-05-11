using UnityEngine;

[System.Serializable]
public class Achievement : ModuleController, IStatus
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int Level { get; private set; }
    [field: SerializeField] public string Tag { get; private set; }
    [field: SerializeField] public Status StatusAbility { get; private set; }
    [field: SerializeField] public AchievementAttributes Attributes { get; private set; }

    public bool CompareTag(string tag)
    {
        return !string.IsNullOrEmpty(tag) && !string.IsNullOrEmpty(Tag) && Tag == tag;
    }
    public void SetName(string name)
    {
        Name = name;
    }
    public void SetAttributes(AchievementAttributes attributes)
    {
        Attributes = attributes;
    }

}