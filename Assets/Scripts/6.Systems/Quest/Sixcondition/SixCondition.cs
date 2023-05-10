﻿[System.Serializable]
public class SixCondition
{
    public string Name { get; private set; }
    public When When { get; private set; }
    public Where Where { get; private set; }
    public Who Who { get; private set; }
    public What What { get; private set; }
    public How How { get; private set; }
    public Why Why { get; private set; }

    public SixCondition(string name,
        string[] deadLines,
        string[] places,
        string[] subjects, string[] minSubjectCounts, string[] maxSubjectCounts,
        string[] targets,
        string[] completionCounts, string[] behaviours,
        string[] rewards, string[] rewardEachCounts, string[] punishments, string[] punishmentEachCounts)
    {
        Name = name;
        When = new When(name, deadLines);
        Where = new Where(name, places);
        Who = new Who(name, subjects, minSubjectCounts, maxSubjectCounts);
        What = new What(name, targets);
        How = new How(name, completionCounts, behaviours);
        Why = new Why(name, rewards, rewardEachCounts, punishments, punishmentEachCounts);
    }

}
