using System.Collections.Generic;
using UnityEngine;

public class CreateNpcAbility : MonoBehaviour
{
    [field: SerializeField] public ExcelCsvReader CsvReader { get; private set; }
    [field:SerializeField] public AbilityResourceInfo[] AllAbilityResourceInfos { get; private set; }

    public void CreateNpcsAbilities()
    {
        foreach (AbilityResourceInfo info in AllAbilityResourceInfos)
        {
            info.LoadExcelDocument(CsvReader);
        }
    }

    private void LoadAbilityInfos(List<string[]> values)
    {
        foreach (string[] rowData in values)
        {
            
        }
    }
}
