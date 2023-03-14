using NaughtyAttributes;
using NUnit.Framework;
using UnityEngine;

public class BattleFormulaInfoTest : MonoBehaviour
{
    // PreCalculateValue_Calculates_PreCalculatedValue
    private BattleFormula formula;
    private BattleFormulaInfo formulaInfo;
    // CalculateFinalValue_Calculates_FinalValue
    private BattleFormula formula1;
    private BattleFormulaInfo formulaInfo1;
    private BattleFormulaInfo formulaInfo2;
    private BattleFormulaInfo formulaInfo3;

    [SetUp, Button]
    public void SetUp()
    {
        formula = ScriptableObject.CreateInstance<BattleFormula>();
        formulaInfo = ScriptableObject.CreateInstance<BattleFormulaInfo>();
        
        formula1 = ScriptableObject.CreateInstance<BattleFormula>();
        formulaInfo1 = ScriptableObject.CreateInstance<BattleFormulaInfo>();
        formulaInfo2 = ScriptableObject.CreateInstance<BattleFormulaInfo>();
        formulaInfo3 = ScriptableObject.CreateInstance<BattleFormulaInfo>();
    }
    [Test, Button]
    public void PreCalculateValue_Calculates_PreCalculatedValue()
    {
        // Arrange
        formulaInfo.CalculationType = CalculationType.Additive;
        formulaInfo.CalculationTargetType = CalculationTargetType.UseModifyValue;
        formulaInfo.StatusName = "attack";
        formulaInfo.ModifyValue = 10;
        formula.FormulaInfoList = new System.Collections.Generic.List<BattleFormulaInfo> { formulaInfo };
        GameObject testObject = new GameObject();
        Status status = testObject.AddComponent<Status>();
        status.SetBaseValue("attack", 20);

        // Act
        formula.PreCalculateValue(status);

        // Assert
        Assert.AreEqual(30f, formulaInfo.GetPreCalculatedValue());
        formulaInfo.Reset();
    }

    [Test, Button]
    public void CalculateFinalValue_Calculates_FinalValue()
    {
        // Arrange
        formulaInfo1.CalculationType = CalculationType.Additive;
        formulaInfo1.CalculationTargetType = CalculationTargetType.UseModifyValue;
        formulaInfo1.StatusName = "attack";
        formulaInfo1.ModifyValue = 10;
        formulaInfo2.CalculationType = CalculationType.Multiply;
        formulaInfo2.CalculationTargetType = CalculationTargetType.UseFormulaInfoValue;
        formulaInfo2.StatusName = "defense";
        formulaInfo2.ModifyBattleFormulaInfo = formulaInfo1;
        formulaInfo3.CalculationType = CalculationType.Multiply;
        formulaInfo3.CalculationTargetType = CalculationTargetType.UseFormulaInfoValue;
        formulaInfo3.StatusName = "speed";
        formulaInfo3.ModifyBattleFormulaInfo = formulaInfo2;
        formula1.FormulaInfoList = new System.Collections.Generic.List<BattleFormulaInfo> { formulaInfo1, formulaInfo2,formulaInfo3 };
        GameObject testObject = new GameObject();
        Status status = testObject.AddComponent<Status>();
        status.SetBaseValue("attack", 20);
        status.SetBaseValue("defense", 2);
        status.SetBaseValue("speed", 4);
        formula1.PreCalculateValue(status);

        // Act
        float finalValue = formula1.CalculateFinalValue();

        // Assert
        Assert.AreEqual(240f, finalValue);
        formulaInfo1.Reset();
        formulaInfo2.Reset();
    }
}
