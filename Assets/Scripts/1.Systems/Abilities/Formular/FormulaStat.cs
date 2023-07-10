using UnityEngine;

[System.Serializable]
public class FormulaStat
{
    [field:SerializeField] public string FormulaName { get;  set; }
    [field:SerializeField] public string StatusName { get;  set; }
    [field:SerializeField] public ReflectStatTarget ReflectStatTarget { get;  set; }
    [field:SerializeField] public float ReflectValue { get;  set; }
    [field:SerializeField] public DataUnitType DataUnitType { get;  set; }
    [field:SerializeField]  float[] BaseValues { get; set; }
    [field:SerializeField] public int Level { get;  set; }
    [field:SerializeField] public int MaxLevel { get;  set; }
    [field:System.NonSerialized] public float CalculatedValue { get;  set; }
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
