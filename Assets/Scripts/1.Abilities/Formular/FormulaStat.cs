using UnityEngine;

[System.Serializable]
public class FormulaStat
{
    [field:SerializeField] public string FormulaName { get; private set; }
    [field:SerializeField] public string StatusName { get; private set; }
    [field:SerializeField] public ReflectStatTarget ReflectStatTarget { get; private set; }
    [field:SerializeField] public float ReflectValue { get; private set; }
    [field:SerializeField] public DataUnitType DataUnitType { get; private set; }
    [field:SerializeField] private float[] BaseValues { get; set; }
    [field:SerializeField] public int Level { get; private set; }
    [field:SerializeField] public int MaxLevel { get; private set; }
    [field:SerializeField] public float CalculatedValue { get; private set; }

    public FormulaStat(string formulaName,string statusName, ReflectStatTarget reflectStatTarget, float reflectValue, DataUnitType dataUnitType, float[] baseValues, int maxLevel)
    {
        FormulaName = formulaName;
        StatusName = statusName;
        ReflectStatTarget = reflectStatTarget;
        ReflectValue = reflectValue;
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

    public float GetCalculatedValue(Character character, Character otherCharacter)
    {
        float baseValue = GetBaseValue();
        float statusValue = 0;
        float modifierValue = 0;
        switch (ReflectStatTarget)
        {
            case ReflectStatTarget.Me:
            {
                statusValue = character.StatusAbility.GetStatusValue(StatusName);
                modifierValue = DataUnitType switch
                {
                    DataUnitType.Numeric => ReflectValue,
                    DataUnitType.Percentage => statusValue * ReflectValue * 0.01f,
                    _ => modifierValue
                };

                CalculatedValue = baseValue + modifierValue;
                break;
            }
            case ReflectStatTarget.Other:
            {
                statusValue = otherCharacter.StatusAbility.GetStatusValue(StatusName);

                modifierValue = DataUnitType switch
                {
                    DataUnitType.Numeric => ReflectValue,
                    DataUnitType.Percentage => statusValue * ReflectValue * 0.01f,
                    _ => modifierValue
                };

                CalculatedValue = baseValue + modifierValue;
                break;
            }
        }

        return CalculatedValue;
    }
    private float GetBaseValue() => BaseValues[Level];
}
