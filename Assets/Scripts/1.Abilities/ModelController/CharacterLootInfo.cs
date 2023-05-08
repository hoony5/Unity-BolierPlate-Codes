using UnityEngine;

[System.Serializable]
public class CharacterLootInfo
{
    [field:SerializeField] public string Name { get; private set; }
    [field:SerializeField] public int LootExp { get; private set; }
    [field:SerializeField] public int LootMoney { get; private set; }
    [field:SerializeField] public string[] LootCommonItems { get; private set; }
    [field:SerializeField] public float LootCommon { get; private set; }
    [field:SerializeField] public string[] LootUncommonItems { get; private set; }
    [field:SerializeField] public float LootUncommon { get; private set; }
    [field:SerializeField] public string[] LootRareItems { get; private set; }
    [field:SerializeField] public float LootRare { get; private set; }
    [field:SerializeField] public string[] LootUniqueItems { get; private set; }
    [field:SerializeField] public float LootUnique { get; private set; }
    [field:SerializeField] public string[] LootLegendaryItems { get; private set; }
    [field:SerializeField] public float LootLegendary { get; private set; }
    [field:SerializeField] public string[] LootMythItems { get; private set; }
    [field:SerializeField] public float LootMyth { get; private set; }
    
    public CharacterLootInfo(string name, int lootExp, int lootMoney, string[] lootCommonItems, float lootCommon, string[] lootUncommonItems, float lootUncommon, string[] lootRareItems, float lootRare, string[] lootUniqueItems, float lootUnique, string[] lootLegendaryItems, float lootLegendary, string[] lootMythItems, float lootMyth)
    {
        Name = name;
        LootExp = lootExp;
        LootMoney = lootMoney;
        LootCommonItems = lootCommonItems;
        LootCommon = lootCommon;
        LootUncommonItems = lootUncommonItems;
        LootUncommon = lootUncommon;
        LootRareItems = lootRareItems;
        LootRare = lootRare;
        LootUniqueItems = lootUniqueItems;
        LootUnique = lootUnique;
        LootLegendaryItems = lootLegendaryItems;
        LootLegendary = lootLegendary;
        LootMythItems = lootMythItems;
        LootMyth = lootMyth;
    }
}
