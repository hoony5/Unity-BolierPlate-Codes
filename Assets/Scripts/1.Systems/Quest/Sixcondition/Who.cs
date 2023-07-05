using UnityEngine;

[System.Serializable]
public class Who
{
    [field:SerializeField] public string Name { get; private set; }
    [field:SerializeField] public string[] Subjects { get; private set; }
    [field:SerializeField] public string[] MinSubjectCount { get; private set; }
    [field:SerializeField] public string[] MaxSubjectCount { get; private set; }
    
    public Who(string name, string[] subjects, string[] minSubjectCount, string[] maxSubjectCount)
    {
        Name = name;
        Subjects = subjects;
        MinSubjectCount = minSubjectCount;
        MaxSubjectCount = maxSubjectCount;
    }
}
