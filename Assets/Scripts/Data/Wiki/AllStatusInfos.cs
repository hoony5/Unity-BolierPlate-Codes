using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// this class is used to store all the status names and their index
/// </summary>
[System.Serializable, CreateAssetMenu(fileName = "newStatusesNames", menuName = "ScriptableObject/Status/StatusesNames")]
public class AllStatusInfos : ScriptableObject
{
    [SerializeField] private StatusItemInfo[] stats;
    private Dictionary<string, int> _statusIndexMap;
    private string[] _statusIndexMapKeys;
    public string[] GetStatusIndexMapKeys => _statusIndexMapKeys;
    
    private void OnEnable()
    {
#if  UNITY_EDITOR
        // SerializedDictionary를 사용해야하나..
#else
        ConvertToDictionary();
        ExtractIndexMapKeys();
#endif
    }

    private void ConvertToDictionary()
    {
        _statusIndexMap = new Dictionary<string, int>(128);
        _statusIndexMap = stats.ToDictionary(key => key.RawName, value => value.Index);
    }
    private void ExtractIndexMapKeys()
    {
        _statusIndexMapKeys = _statusIndexMap.Keys.ToArray();
    }
    
    // for update the specific Status value
    public int GetStatusIndex(string name) => _statusIndexMap[name];
}