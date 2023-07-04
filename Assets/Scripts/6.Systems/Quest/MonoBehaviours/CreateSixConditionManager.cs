using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility.ExcelReader;

[System.Serializable]
public class CreateSixConditionManager : MonoBehaviour
{
    [field:SerializeField] public ExcelCsvReader ExcelCsvReader { get; private set; }
    [field:SerializeField] public AbilityResourceInfo[] AbilityResourceInfos { get; private set; }
    private Dictionary<string, SixCondition> allSixConditionsMap;

    public SixCondition TryGetSixCondition(string name)
    {
        bool exist = allSixConditionsMap.TryGetValue(name, out SixCondition sixCondition);
        return exist ? sixCondition : null;
    }
    
    public void LoadBattleFormulas()
    {
        List<SixCondition> result = new List<SixCondition>(64);
        foreach (AbilityResourceInfo info in AbilityResourceInfos)
        {
            // add info sheet name
            result.AddRange(LoadAllSixthConditions(info.GetDataList()));
        }
        allSixConditionsMap = result.ToDictionary(key => key.Name, value => value);
    }

    private List<SixCondition> LoadAllSixthConditions(List<string[]> values)
    {
        List<SixCondition> sixConditions = new List<SixCondition>(values.Count);

        List<string> deadLines = new List<string>(values.Count);
        List<string> places = new List<string>(values.Count);
        List<string> subjects = new List<string>(values.Count);
        List<string> minSubjectCounts = new List<string>(values.Count);
        List<string> maxSubjectCounts = new List<string>(values.Count);
        List<string> targets = new List<string>(values.Count);
        List<string> completionCounts = new List<string>(values.Count);
        List<string> behaviours = new List<string>(values.Count);
        List<string> rewards = new List<string>(values.Count);
        List<string> rewardEachCounts = new List<string>(values.Count);
        List<string> punishments = new List<string>(values.Count);
        List<string> punishmentEachCounts = new List<string>(values.Count);
        
        string currentCondition = string.Empty;
        string nextCondition = string.Empty;
        
        for (var index = 0; index < values.Count; index++)
        {
            string[] rowDatas = values[index];

            currentCondition = string.IsNullOrEmpty(rowDatas[0]) ? currentCondition : rowDatas[0];
            nextCondition = index < values.Count - 1 ? values[index + 1][0] : currentCondition;

            if (!string.IsNullOrEmpty(nextCondition))
            {
                if (index != values.Count - 1 && string.IsNullOrEmpty(nextCondition)) continue;
                SixCondition condition = new SixCondition
                (
                    name: currentCondition,
                    deadLines: deadLines.ToArray(),
                    places: places.ToArray(),
                    subjects: subjects.ToArray(),
                    minSubjectCounts: minSubjectCounts.ToArray(),
                    maxSubjectCounts: maxSubjectCounts.ToArray(),
                    targets: targets.ToArray(),
                    completionCounts: completionCounts.ToArray(),
                    behaviours: behaviours.ToArray(),
                    rewards: rewards.ToArray(),
                    rewardEachCounts: rewardEachCounts.ToArray(),
                    punishments: punishments.ToArray(),
                    punishmentEachCounts: punishmentEachCounts.ToArray()
                );
                
                if(!sixConditions.Exists(i => i.Name == condition.Name))
                    sixConditions.Add(condition);
            }
            else
            {
                deadLines.Add(string.IsNullOrEmpty(rowDatas[1]) ? "None": rowDatas[1]);
                places.Add(string.IsNullOrEmpty(rowDatas[2]) ? "None": rowDatas[2]);
                subjects.Add(rowDatas[3]);
                minSubjectCounts.Add(string.IsNullOrEmpty(rowDatas[4]) ? "1": rowDatas[4]);
                maxSubjectCounts.Add(string.IsNullOrEmpty(rowDatas[5]) ? "1": rowDatas[5]);
                targets.Add(rowDatas[6]);
                completionCounts.Add(string.IsNullOrEmpty(rowDatas[7]) ? "1": rowDatas[7]);
                behaviours.Add(rowDatas[8]);
                rewards.Add(rowDatas[9]);
                rewardEachCounts.Add(string.IsNullOrEmpty(rowDatas[10]) ? "1" : rowDatas[10]);
                punishments.Add(rowDatas[11]);
                punishmentEachCounts.Add(string.IsNullOrEmpty(rowDatas[12]) ? "1" : rowDatas[12]);
            }
        }

        return sixConditions;
    }
    
}
