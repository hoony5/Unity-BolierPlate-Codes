using UnityEngine;

[System.Serializable]
public class When
{
    [field:SerializeField] public string Name { get; private set; }
    [field:SerializeField] public string[] DeadLines { get; private set; }
    
    public When(string name, string[] deadLines)
    {
        Name = name;
        DeadLines = deadLines;
    }
}
