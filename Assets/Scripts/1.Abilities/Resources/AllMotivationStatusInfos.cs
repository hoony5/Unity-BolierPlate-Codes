using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "newEffectMotivationStatuses", menuName = "ScriptableObject/Effect/EffectMotivationStatuses", order = 0)]
public class AllMotivationStatusInfos : ScriptableObject
{
    [field:SerializeField]  public List<MotivationInfo> MotivationInfos { get; set; }
    private Dictionary<string, MotivationInfo> _motivationInfosMap = new Dictionary<string, MotivationInfo>(64);

    private void OnEnable()
    {
        Reset();
    }

    public void Reset()
    {
        _motivationInfosMap.Clear();
        _motivationInfosMap = MotivationInfos.ToDictionary(key => key.EffectName, value => value);
    }

    public void SetEffectInfomations(MotivationInfo[] searchStatuses)
    {
        MotivationInfos.AddRange(searchStatuses);
    }
}