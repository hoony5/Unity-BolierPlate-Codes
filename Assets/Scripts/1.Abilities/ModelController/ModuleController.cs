using UnityEngine;

public abstract class ModuleController : MonoBehaviour
{
    [field: SerializeField] public int ObjectIndex { get; set; } = -1;
    [field: SerializeField] public int NetworkIndex { get; set; } = -1;
}