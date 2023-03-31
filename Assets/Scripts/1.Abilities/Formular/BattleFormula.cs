﻿using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable, CreateAssetMenu(fileName = "New Formula", menuName = "ScriptableObject/Calculate/BattleFormula")]
public class BattleFormula : ScriptableObject
{
    [SerializeField] private string formulaName;
    [SerializeField] private List<BattleFormulaInfo> formulaInfoList;
    [SerializeField] private string description;
    public string FormulaName => formulaName;
    public List<BattleFormulaInfo> FormulaInfoList
    {
        get => formulaInfoList;
        set => formulaInfoList = value;
    }

    // first
    public void PreCalculateValue(Character refMine, Character refOther)
    {
        // Pre-calculate values for performance optimization
        foreach (BattleFormulaInfo formulaInfo in formulaInfoList)
        {
            formulaInfo.CalculatePreCalculatedValue(refMine, refOther);
        }
    }

    //Second
    public float ExecuteFormula()
    {
        float finalValue = 0f;

        // Calculate final value using pre-calculated values
        foreach (BattleFormulaInfo formulaInfo in formulaInfoList)
        {
            float calculatedValue = formulaInfo.GetPreCalculatedValue();

            // Check if value is valid
            if (float.IsNaN(calculatedValue) || float.IsInfinity(calculatedValue))
            {
                Debug.LogError(
                    $"Invalid or missing value detected in formula: {formulaName}, formula info: {formulaInfo.name}!");
                continue;
            }

            // Update Latest Value
            finalValue = calculatedValue;
        }

        return finalValue;
    }
}
