using UnityEngine;

[System.Serializable]
public class TraitsCreator
{
    [field: SerializeField] public ExcelCsvReader CsvReader { get; private set; }
    [field: SerializeField] public AbilityResourceInfo[] AllAbilityResourceInfos { get; private set; }   
}
