using UnityEngine;

[System.Serializable]
public class How
{
    [field:SerializeField] public string Name { get; private set; }
    [field:SerializeField] public string[] CompletionCounts { get; private set; }
    [field:SerializeField] public string[] Behaviours { get; private set; }
    
    public How(string name, string[] completionCounts, string[] behaviours)
    {
        Name = name;
        CompletionCounts = completionCounts;
        Behaviours = behaviours;
    }
}
