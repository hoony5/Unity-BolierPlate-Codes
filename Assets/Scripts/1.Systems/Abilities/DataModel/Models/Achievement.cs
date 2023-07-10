using UnityEngine;

[System.Serializable]
public class Achievement : ModuleController
{
    [field: SerializeField] public string Name { get;  set; }
    [field: SerializeField] public Status StatusAbility { get;  set; }
    [field: SerializeField] public AchievementAttributes Attributes { get;  set; }
}