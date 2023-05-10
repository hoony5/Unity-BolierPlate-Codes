using UnityEngine;

[System.Serializable]
public class Pet : Character
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public NPCAttributes Attributes { get; private set; }

    public void SetName(string name)
    {
        Name = name;
    }

    public void SetAttributes(NPCAttributes attributes)
    {
        Attributes = attributes;
    }
}