using AYellowpaper.SerializedCollections;
using UnityEngine;

public class FormulaManager : Singleton<FormulaManager>
{
    [field: SerializeField]
    private SerializedDictionary<string, BattleFormulaInfo> FormulaMap { get; } =
        new SerializedDictionary<string, BattleFormulaInfo>();


    public bool TryGetFormula(string formulaName, out BattleFormulaInfo formula)
    {
        formula = null;
        if(FormulaMap.TryGetValue(formulaName, out formula))
            return true;
#if UNITY_EDITOR
        Debug.LogError($"FormulaManager: {formulaName} is not exist.");
#endif
        return false;
    }
}
