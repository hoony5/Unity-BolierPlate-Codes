using UnityEngine;

[System.Serializable]
public class NPCLootInfo
{
    [field:SerializeField] public string Name { get; set; }
    [field:SerializeField] public int LootExp { get; set; }
    [field:SerializeField] public int LootMoney { get; set; }
    [field:SerializeField] public string[] LootCommonItems { get; set; }
    [field:SerializeField] public float LootCommon { get; set; }
    [field:SerializeField] public string[] LootUncommonItems { get; set; }
    [field:SerializeField] public float LootUncommon { get; set; }
    [field:SerializeField] public string[] LootRareItems { get; set; }
    [field:SerializeField] public float LootRare { get; set; }
    [field:SerializeField] public string[] LootUniqueItems { get; set; }
    [field:SerializeField] public float LootUnique { get; set; }
    [field:SerializeField] public string[] LootLegendaryItems { get; set; }
    [field:SerializeField] public float LootLegendary { get; set; }
    [field:SerializeField] public string[] LootMythItems { get; set; }
    [field:SerializeField] public float LootMyth { get; set; }
}
