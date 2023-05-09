using System.Collections.Generic;
using UnityEngine;

[ToDo("After Adding Addressable Asset, Work on"), System.Serializable]
public class BehaviourReferenceManager : MonoBehaviour
{
    [field:SerializeField] public AbilityResourceInfo[] AbilityResourceInfos { get; private set; }
    [SerializeField] private List<BehaviourReferenceInfo> effectReferenceInfos = new List<BehaviourReferenceInfo>();
    private Dictionary<string, BehaviourReferenceInfo> _effectReferenceInfosMap = new Dictionary<string, BehaviourReferenceInfo>();

    public void SetBehaviourReferenceInfo(ref List<BattleBehaviour> input)
    {
        Init();

        foreach (BattleBehaviour data in input)
        {
            if (!_effectReferenceInfosMap.ContainsKey(data.BehaviourName)) continue;
            data.BehaviourReferenceInfo = _effectReferenceInfosMap[data.BehaviourName];
        }
    }
    private void Init()
    {
        foreach (AbilityResourceInfo info in AbilityResourceInfos)
        {
            List<string[]> data = info.GetDataList();
            GetEffectLevelInfo(data);
        }
        foreach (BehaviourReferenceInfo valueInfo in effectReferenceInfos)
        {
            if (_effectReferenceInfosMap.ContainsKey(valueInfo.BehaviourName)) continue;
            _effectReferenceInfosMap.Add(valueInfo.BehaviourName, valueInfo);
        }
    }

    public void GetEffectLevelInfo(List<string[]> values)
    {
        for (int index = 0; index < values.Count; index++)
        {
            string[] rowDatas = values[index];
            effectReferenceInfos[index].BehaviourName = rowDatas[0];
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