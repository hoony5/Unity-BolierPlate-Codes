using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MotivationInfo
{
    [field: SerializeField] public string EffectName { get; set; }
    [field: SerializeField] public List<MotivationStatusInfo> MotivationStatusInfos{ get; set; }
    public MotivationInfo(){ }
    public MotivationInfo(string effectName, List<MotivationStatusInfo> motivationStatusInfos)
    {
        EffectName = effectName;
        MotivationStatusInfos = motivationStatusInfos;
    }
}