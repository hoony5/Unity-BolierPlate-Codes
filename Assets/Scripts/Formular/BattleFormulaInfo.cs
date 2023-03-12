using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Formula Info", menuName = "ScriptableObject/Calculate/BattleFormulaInfo")]
public class BattleFormulaInfo : ScriptableObject
{
    [SerializeField] private string statusName;
    [SerializeField] private CalculationTargetType calculationTargetType;
    [SerializeField] private BattleFormulaInfo modifyBattleFormulaInfo;
    [SerializeField] private CalculationType calculationType;
    [SerializeField] private float modifyValue;
    [SerializeField] private string description;
    [SerializeField] private bool useInputMode;
    [SerializeField] private bool useClamp;
    [SerializeField] private Vector2 minMaxRange;
    [NonSerialized] public float baseValue; // baseValue is determined by statusName
    [NonSerialized] public float calculatedValue; // Pre-calculated value for performance optimization
    
    public CalculationType CalculationType { get => calculationType; set => calculationType = value; }
    public CalculationTargetType CalculationTargetType { get => calculationTargetType; set => calculationTargetType = value; }
    public BattleFormulaInfo ModifyBattleFormulaInfo { get => modifyBattleFormulaInfo; set => modifyBattleFormulaInfo = value; }
    public string StatusName { get => statusName; set => statusName = value; }
    public float ModifyValue { get => modifyValue; set => modifyValue = value; }
    
    public void CalculatePreCalculatedValue(Status status)
    {
        baseValue = status.GetFinalizeValue(statusName);
        calculatedValue = calculationType switch
        {
            CalculationType.None => 0f,
            CalculationType.Additive => baseValue + GetResult(),
            CalculationType.Multiply => baseValue * GetResult(),
            CalculationType.Logarithmic => Mathf.Log(baseValue, GetResult()),
            _ => 0
        };
        calculatedValue = useClamp ? Mathf.Clamp(calculatedValue, minMaxRange.x, minMaxRange.y) : calculatedValue;
    }
#if UNITY_EDITOR
    public float GetEditorCalculatedValue()
    {
        float result = calculationType switch
        {
            CalculationType.None => 0f,
            CalculationType.Additive => baseValue + GetResult(),
            CalculationType.Multiply => baseValue * GetResult(),
            CalculationType.Logarithmic => Mathf.Log(baseValue, GetResult()),
            _ => 0
        };
        return useClamp ? Mathf.Clamp(result, minMaxRange.x, minMaxRange.y) : result;
    }
#endif
    private float GetResult()
    {
        switch (calculationTargetType)
        {
            case CalculationTargetType.UseModifyValue:
                return modifyValue;
            case CalculationTargetType.UseFormulaInfoValue when modifyBattleFormulaInfo is not null:
                float result = 0;
#if UNITY_EDITOR
                result = modifyBattleFormulaInfo.GetEditorCalculatedValue();
#else
                return = modifyBattleFormulaInfo.GetPreCalculatedValue();
#endif
                return result;
            case CalculationTargetType.UseFormulaInfoValue:
                Debug.LogWarning("Missing formula reference");
                return 0;
            case CalculationTargetType.UseSelfValue:
                return baseValue;
            default:
                return 0f;
        }
    }
    public float GetPreCalculatedValue()
    {
        return calculatedValue;
    }

    public void Reset()
    {
        calculationTargetType = CalculationTargetType.UseModifyValue;
        modifyBattleFormulaInfo = null;
        CalculationType = CalculationType.None;
    }

    public string ToFormulaString()
    {
        string modifier = calculationTargetType switch
        {
            CalculationTargetType.UseModifyValue => modifyValue.ToString(),
            CalculationTargetType.UseSelfValue => baseValue.ToString(),
            CalculationTargetType.UseFormulaInfoValue => modifyBattleFormulaInfo is null
                ? "Please, assign BattleFormulaInfo"
                : modifyBattleFormulaInfo.GetEditorCalculatedValue().ToString(),
            _ => throw new ArgumentOutOfRangeException()
        };
        string @operator = calculationType switch
        {
            CalculationType.None => " None ",
            CalculationType.Additive => " + ",
            CalculationType.Multiply => " x ",
            CalculationType.Logarithmic => "Log ",
            _ => throw new ArgumentOutOfRangeException()
        };
        string clampedValue = "";
        if (useClamp)
        {
            if(GetEditorCalculatedValue() <= minMaxRange.x)
                clampedValue = $"Clamped Min(<color=yellow>{minMaxRange.x}</color>)";
            else if (GetEditorCalculatedValue() >= minMaxRange.y)
                clampedValue = $"Clamped Max(<color=yellow>{minMaxRange.y}</color>)";
        }

        string resultValue = "";
        if (calculationType is CalculationType.Logarithmic)
        {
            resultValue = $"Log({baseValue}) {modifier})";
            return $"{(useClamp ? $"{clampedValue} : {resultValue}" : $"{clampedValue}")}";
        }
        
        resultValue = $"{baseValue} {@operator} {modifier}";
        return $"{(useClamp ? $"{clampedValue} : {resultValue}" : $"{clampedValue}")}";
    }
}