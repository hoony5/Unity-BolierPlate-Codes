using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Utility.ExcelReader;
using Utility.ExcelReader.Editor;

public class TestC : MonoBehaviour
{
    public RowData rowData;
    //public SerializeDictionary<string, string> test = new SerializeDictionary<string, string>();
    public ExpenseAbilityInfo expenseAbilityInfoData;

    [Button()]
    private void Parse()
    {
        // expenseAbilityInfoData = ExcelDataParser.FromData<ExpenseAbilityInfo>(rowData);
        _ = TestSingleton.Instance.name;
    }
}

