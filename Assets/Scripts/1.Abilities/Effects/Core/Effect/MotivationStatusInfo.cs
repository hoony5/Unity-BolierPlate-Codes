using UnityEngine;

[System.Serializable]
public class MotivationStatusInfo
{
    // active or not
    [field:SerializeField] public bool MotivationActive { get; set; }
    // is reflected by the status of the character my or his/her
    [field:SerializeField] public bool HasReflectMyStatus { get; set; }
    [field:SerializeField] public bool HasReflectMaxStatus { get; set; }
    // what kind of current StatName (ex. currentHp)
    [field:SerializeField] public string CurrentStatName { get; set; }
    // what kind of max StatName (ex. maxHp)
    [field:SerializeField] public string MaxStatName { get; set; }
    // how much value is reflected by the status of the character
    [field:SerializeField] public float ReflectValue { get; set; }
    // value Unit is either percent or numeric
    [field:SerializeField] public DataUnitType ReflectValueUnitType { get; set; }
    [field:SerializeField] public string MotivatedStatName { get; set; }
    [field:SerializeField] public float MotivatedValue { get; set; }
    [field:SerializeField] public DataUnitType MotivatedValueUnitType { get; set; }
    // compare Type ( equal or less or greater )
    [field:SerializeField] public ComparerType MotivationComparerType { get; set; }
    // Calculation Value Type
    [field:SerializeField] public CalculationType CalculationType { get; set; }
    [field:SerializeField] public ApplyTargetType ApplyTargetType { get; set; }
    [field:SerializeField] public float AppliedValue { get; set; }
}