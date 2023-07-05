using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Utility.ExcelReader;
using Utility.ExcelReader.Editor;

public class TestC : MonoBehaviour
{
    public RowData rowData;
    public GameObject go;
    public ExpenseAbilityInfo expenseAbilityInfoData;
    public string a1;
    public string a2;
    public string a3;

    [Button()]
    private void Parse()
    {
        //expenseAbilityInfoData = ExcelDataParser.FromData<ExpenseAbilityInfo>(rowData);

        Debug.Log(!go);
    }
}
