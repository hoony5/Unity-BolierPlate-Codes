using UnityEngine;

[System.Serializable]
public class Where
{
    [field:SerializeField] public string Name { get; private set; }
    [field:SerializeField] public string[] Places { get; private set; }
    
    public Where(string name, string[] places)
    {
        Name = name;
        Places = places;
    }
}
