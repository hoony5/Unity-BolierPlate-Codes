using UnityEngine;

[System.Serializable]
public abstract class ModuleController
{
    [field: SerializeField] public int ObjectIndex { get; set; } = -1;
    [field: SerializeField] public int NetworkIndex { get; set; } = -1;
}