using UnityEngine;

[System.Serializable]
public class FormulaStat
{
    [field:SerializeField] public string StatusName { get; private set; }
    [field:SerializeField] public ReflectStatTarget ReflectStatTarget { get; private set; }
    [field:SerializeField] public float ReflectValue { get; private set; }
    [field:SerializeField] public CalculationType CalculationType { get; private set; }
    [field:SerializeField] public DataUnitType DataUnitType { get; private set; }
    [field:SerializeField] private float[] BaseValues { get; set; }
    [field:SerializeField] public int Level { get; private set; }
    [field:SerializeField] public int MaxLevel { get; private set; }
    [field:SerializeField] public float CalculatedValue { get; private set; }

    public FormulaStat(string statusName, ReflectStatTarget reflectStatTarget, float reflectValue, CalculationType calculationType, DataUnitType dataUnitType, float[] baseValues, int maxLevel)
    {
        StatusName = statusName;
        ReflectStatTarget = reflectStatTarget;
        ReflectValue = reflectValue;
        CalculationType = calculationType;
        DataUnitType = dataUnitType;
        BaseValues = baseValues;
        Level = 1;
        MaxLevel = maxLevel;
    }

    public void SetLevel(int lv)
    {
        Level = lv;
        if(Level > MaxLevel) Level = MaxLevel;
    }

    public float GetCalculatedValue()
    {
        
        return CalculatedValue ;//= 
    }
    private float GetBaseValue() => BaseValues[Level];
}
