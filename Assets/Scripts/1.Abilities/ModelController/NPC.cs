using UnityEngine;

[System.Serializable]
public class NPC : Character
{
    [field:SerializeField] public string Name { get; private set; }
    [field:SerializeField] public CharacterAttributes Attributes { get; private set; }
    [field:SerializeField] public CharacterLootInfo LootInfo { get; private set; }

    public void SetName(string name)
    {
        Name = name;   
    }
    public void SetAttributes(CharacterAttributes attributes)
    {
        Attributes = attributes;
    }
    public void SetLootInfo(CharacterLootInfo lootInfo)
    {
        LootInfo = lootInfo;
    }
}
