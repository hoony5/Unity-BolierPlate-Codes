using System.Collections.Generic;
using UnityEngine;

public class EffectAbilityManager : MonoBehaviour
{
    public AbilityResourceInfo[] abilityResourceInfos;

    [ToDo("모든 데이터를 설정함에 있어서, 이미 로드 되어있는 기존의 Effect 데이터에 적확하게 개입되어야한다. 인수로 Effect 리스트를 받는편이 좋을 것 같다.")]
    public List<EffectAbility> LoadAllAbilityInfos()
    {
        foreach (AbilityResourceInfo info in abilityResourceInfos)
        {
            List<string[]> infos = info.GetAbilityDatas();
            var loadedData = LoadEffectAbilityInfo(infos);
        }

        return null;
    }
    [ToDo("중첩되는 효과에 대해서 여러 행의 데이터가 입력될때, 끊어줘야한다. rowData[0]의 값이 Empty라면 계속 데이터를 Add하고 그렇지 않다면 다음으로 넘긴다.")]
    private List<EffectAbilityMap> LoadEffectAbilityInfo(List<string[]> values)
    {
        List<EffectAbilityMap> result = new List<EffectAbilityMap>(32);
        List<EffectAbility> effectAbilities = new List<EffectAbility>(values.Count);
        string currentEffectName = string.Empty;
        string nextEffectName = string.Empty;
        
        string currentAbilityName = string.Empty;
        string nextAbilityName = string.Empty;

        for (int index = 0; index < values.Count - 1; index++)
        {
            string[] rowDatas = values[index];

            // 처음인지 확인
            if (index == 0)
            {
                currentEffectName = rowDatas[0];
                currentAbilityName = rowDatas[1];
            }

            nextEffectName = values[index + 1][0];
            nextAbilityName = values[index + 1][1];
                // 내용 추가
            if (string.IsNullOrEmpty(rowDatas[0]))
            {
                /*
                    abilityName: rowDatas[1],
                */
                EffectAbility effectAbility = new EffectAbility(
                    statRawName: rowDatas[2],
                    applyTargetType: rowDatas[3],
                    calculationType: rowDatas[4],
                    value: float.TryParse(rowDatas[5], out float Value) ? Value : 0,
                    min: float.TryParse(rowDatas[6], out float Min) ? Min : 0,
                    max: float.TryParse(rowDatas[7], out float Max) ? Max : 0
                );
                effectAbilities.Add(effectAbility);
            }

            // 다음인지 확인

            if (!string.IsNullOrEmpty(currentEffectName) &&
                !string.IsNullOrEmpty(nextEffectName) &&
                currentEffectName != nextEffectName)
            {
                currentEffectName = nextEffectName;
            }

            if (!string.IsNullOrEmpty(currentAbilityName) &&
                !string.IsNullOrEmpty(nextAbilityName) &&
                currentAbilityName != nextAbilityName)
            {
                currentAbilityName = nextAbilityName;
                // 여기부터 다시시작
                EffectAbilityMap saveData = new EffectAbilityMap()
                {
                    effectName = currentEffectName,
                    abilityName = currentAbilityName,
                    effectAbilities = effectAbilities
                };
                result.Add(saveData);
                effectAbilities.Clear();
            }
        }

        return effectAbilities;
    }

}   