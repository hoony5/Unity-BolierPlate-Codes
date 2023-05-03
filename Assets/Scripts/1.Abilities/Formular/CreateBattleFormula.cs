using UnityEngine;

[System.Serializable]
public class CreateBattleFormula
{
    [field:SerializeField] public ExcelCsvReader CsvReader { get; private set; }
    [field: SerializeField] public BattleFormulaInfoManager BattleFormulaInfoManager { get; private set; }
    [field:SerializeField] public BattleFormulaStatManager BattleFormulaStatManager { get; private set; }

    public void CombineFormulaStatsWithFormulaInfo()
    {
        LoadDocuments(BattleFormulaInfoManager.AbilityResourceInfos);
        LoadDocuments(BattleFormulaStatManager.AbilityResourceInfos);
        
        BattleFormulaInfoManager.LoadBattleFormulas();
        BattleFormulaStatManager.LoadBattleFormulas();

        foreach (BattleFormulaInfo statInfo in BattleFormulaInfoManager.AllBattleFormulas)
        {
            if (BattleFormulaStatManager.TryGetValue(statInfo.Name, out FormulaStat stat))
            {
                statInfo.FormulaStats.Add(stat);
            }   
        }
    }
    private void LoadDocuments(AbilityResourceInfo[] abilityResourceInfos)
    {
        foreach (AbilityResourceInfo info in abilityResourceInfos)
        {
            info.LoadExcelDocument(CsvReader);
        }    
    }
}
