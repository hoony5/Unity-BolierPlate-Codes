using System;
using UnityEngine;

namespace Utility.ExcelReader
{
    [Serializable]
    public class DataManager : Singleton<DataManager>
    {
        [field:SerializeField] public ExcelDataSO DataSo { get; set; }
    }
}