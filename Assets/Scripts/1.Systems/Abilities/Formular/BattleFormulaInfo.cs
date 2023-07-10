using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleFormulaInfo
{
    [field: SerializeField] public string Name { get; set; }
    [field: SerializeField] public bool UseClampValue { get; set; }
    [field: SerializeField] public List<FormulaStat> FormulaStats { get; set; }
    [field: SerializeField] public int Min { get; set; }
    [field: SerializeField] public int Max { get; set; }
    [field: SerializeField] public string Description { get; set; }
    
    public BattleFormulaInfo(string name, bool useClampValue, int min, int max, string description)
    {
        Name = name;
        UseClampValue = useClampValue;
        FormulaStats = new List<FormulaStat>(6);
        Min = min;
        Max = max;
        Description = description;
    }
    public float GetCalculatedValue(Character character, Character other)
    {
        float result = 0;
        foreach (FormulaStat stat in FormulaStats)
        {
            result += stat.GetCalculatedValue(character, other);
        }

        return UseClampValue? Mathf.Clamp(result, Min, Max) : result;
    }
}