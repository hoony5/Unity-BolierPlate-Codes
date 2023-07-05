using UnityEngine;

[System.Serializable]
public class ExpenseAbilityStat
{
    [field:SerializeField] public bool IsContinuousCost { get; set; }
    [field:SerializeField] public string ReflectCurrentStatName { get; set; }
    [field:SerializeField] public string ReflectMaxStatName { get; set; }
    [field:SerializeField] public float Cost { get; set; }
    // role instead of Index, if send to server level return cost
    [field:SerializeField] public int CurrentLevel { get; set; }
    // role instead of Max Index
    [field:SerializeField] public int MaxLevel { get; set; }
    [field:SerializeField] public DataUnitType DataUnitType { get; set; }
    [field:SerializeField] public CalculationType CalculationType { get; set; }
}
