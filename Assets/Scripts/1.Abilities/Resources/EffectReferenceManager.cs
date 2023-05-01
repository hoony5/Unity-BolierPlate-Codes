using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EffectReferenceManager : MonoBehaviour
{
    public AbilityResourceInfo[] abilityResourceInfos;
    [SerializeField] private List<EffectReferenceInfo> effectReferenceInfos = new List<EffectReferenceInfo>();
    private Dictionary<string, EffectReferenceInfo> _effectReferenceInfosMap = new Dictionary<string, EffectReferenceInfo>();

    public void SetEffectLevelInfo(ref List<Effect> input)
    {
        Init();

        foreach (Effect data in input)
        {
            if (!_effectReferenceInfosMap.ContainsKey(data._effectName)) continue;
            data.ReferenceInfo = _effectReferenceInfosMap[data._effectName];
        }
    }
    private void Init()
    {
        foreach (AbilityResourceInfo info in abilityResourceInfos)
        {
            List<string[]> data = info.GetAbilityDatas();
            GetEffectLevelInfo(data);
        }
        foreach (EffectReferenceInfo valueInfo in effectReferenceInfos)
        {
            if (_effectReferenceInfosMap.ContainsKey(valueInfo.EffectName)) continue;
            _effectReferenceInfosMap.Add(valueInfo.EffectName, valueInfo);
        }
    }

    public void GetEffectLevelInfo(List<string[]> values)
    {
        for (int index = 0; index < values.Count; index++)
        {
            string[] rowDatas = values[index];
            effectReferenceInfos[index].EffectName = rowDatas[0];
            bool IsParticleOrNot = bool.TryParse(rowDatas[1], out bool isParticleOrNot) && isParticleOrNot;

            if (isParticleOrNot)
            {
                //this is asset address
                // effectReferenceInfos[index].ParticleSystem = rowDatas[2];
                effectReferenceInfos[index].VFX = null;
            }
            else
            {
                // effectReferenceInfos[index].ParticleSystem = null;
                // effectReferenceInfos[index].VFX = rowDatas[2];
            }
            
            // addressable path
            // effectReferenceInfos[index].Icon = rowDatas[3];
            effectReferenceInfos[index].Description = rowDatas[4];
            
        }
    }
}