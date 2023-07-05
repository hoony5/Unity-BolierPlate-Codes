using UnityEngine;

[System.Serializable]
public class What
{
    [field:SerializeField] public string Name { get; private set; }
    [field:SerializeField] public string[] Targets { get; private set; }
    
    public What(string name, string[] targets)
    {
        Name = name;
        Targets = targets;
    }
}
