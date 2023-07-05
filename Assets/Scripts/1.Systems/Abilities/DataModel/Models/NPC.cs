using UnityEngine;

[System.Serializable]
public class NPC : Character
{
    [field:SerializeField] public string Name { get; private set; }
    [field:SerializeField] public NPCAttributes Attributes { get; private set; }
    [field:SerializeField] public NPCLootInfo LootInfo { get; private set; }

    public void SetName(string name)
    {
        Name = name;   
    }
    public void SetAttributes(NPCAttributes attributes)
    {
        Attributes = attributes;
    }
    public void SetLootInfo(NPCLootInfo lootInfo)
    {
        LootInfo = lootInfo;
    }
}
