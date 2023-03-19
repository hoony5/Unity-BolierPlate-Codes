using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace BattleFormulaCore
{

    [CreateAssetMenu(fileName = "New Formula Info", menuName = "ScriptableObject/Calculate/BattleFormulaInfo")]
    public class BattleFormulaInfo : ScriptableObject
    {
        [SerializeField] private string statusName;
        [SerializeField] private CalculationTargetType calculationTargetType;
        [SerializeField] private BattleFormulaInfo modifyBattleFormulaInfo;
        [FormerlySerializedAs("calculationType")] [SerializeField] private FormulaCalculationType formulaCalculationType;
        [SerializeField] private float modifyValue;
        [SerializeField] private string description;
        [SerializeField] private bool useInputMode;
        [SerializeField] private bool useClamp;
        [SerializeField] private Vector2 minMaxRange;
        [NonSerialized] public float baseValue; // baseValue is determined by statusName
        [NonSerialized] public float calculatedValue; // Pre-calculated value for performance optimization

        public FormulaCalculationType FormulaCalculationType
        {
            get => formulaCalculationType;
            set => formulaCalculationType = value;
        }

        public CalculationTargetType CalculationTargetType
        {
            get => calculationTargetType;
            set => calculationTargetType = value;
        }

        public BattleFormulaInfo ModifyBattleFormulaInfo
        {
            get => modifyBattleFormulaInfo;
            set => modifyBattleFormulaInfo = value;
        }

        public string StatusName
        {
            get => statusName;
            set => statusName = value;
        }

        public float ModifyValue
        {
            get => modifyValue;
            set => modifyValue = value;
        }

        public void CalculatePreCalculatedValue(Status status)
        {
            baseValue = status.GetFinalizeValue(statusName);
            calculatedValue = formulaCalculationType switch
            {
                FormulaCalculationType.None => 0f,
                FormulaCalculationType.Additive => baseValue + GetResult(),
                FormulaCalculationType.Multiply => baseValue * GetResult(),
                FormulaCalculationType.Logarithmic => Mathf.Log(baseValue, GetResult()),
                _ => 0
            };
            calculatedValue = useClamp ? Mathf.Clamp(calculatedValue, minMaxRange.x, minMaxRange.y) : calculatedValue;
        }
#if UNITY_EDITOR
        public float GetEditorCalculatedValue()
        {
            float result = formulaCalculationType switch
            {
                FormulaCalculationType.None => 0f,
                FormulaCalculationType.Additive => baseValue + GetResult(),
                FormulaCalculationType.Multiply => baseValue * GetResult(),
                FormulaCalculationType.Logarithmic => Mathf.Log(baseValue, GetResult()),
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
            FormulaCalculationType = FormulaCalculationType.None;
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
            string @operator = formulaCalculationType switch
            {
                FormulaCalculationType.None => " None ",
                FormulaCalculationType.Additive => " + ",
                FormulaCalculationType.Multiply => " x ",
                FormulaCalculationType.Logarithmic => "Log ",
                _ => throw new ArgumentOutOfRangeException()
            };
            string clampedValue = "";
            if (useClamp)
            {
                if (GetEditorCalculatedValue() <= minMaxRange.x)
                    clampedValue = $"Clamped Min(<color=yellow>{minMaxRange.x}</color>)";
                else if (GetEditorCalculatedValue() >= minMaxRange.y)
                    clampedValue = $"Clamped Max(<color=yellow>{minMaxRange.y}</color>)";
            }

            string resultValue = "";
            if (formulaCalculationType is FormulaCalculationType.Logarithmic)
            {
                resultValue = $"Log({baseValue}) {modifier})";
                return $"{(useClamp ? $"{clampedValue} : {resultValue}" : $"{clampedValue}")}";
            }

            resultValue = $"{baseValue} {@operator} {modifier}";
            return $"{(useClamp ? $"{clampedValue} : {resultValue}" : $"{clampedValue}")}";
        }
    }
}