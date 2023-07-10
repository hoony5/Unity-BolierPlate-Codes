using System;
using UnityEngine;

[Serializable]
public class ItemAttributes
{
    [field:SerializeField] public string Name { get; set; }
    [field:SerializeField] public ElementalType ElementalType { get; set; }
    [field:SerializeField] public string Category { get; set; }
    [field:SerializeField] public Grade Grade { get; set; }
    [field: SerializeField] public bool IsQuestItem { get; set; }
    [field: SerializeField] public int MaxCount { get; set; }
    [field:SerializeField] public string Description { get; set; }
}