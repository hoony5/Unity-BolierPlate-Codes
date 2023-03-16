using UnityEngine;

[System.Serializable]
public class SkillItemInfo
{
    [SerializeField] private StatusItemInfo statusItemInfo;
    public StatusItemInfo StatusItemInfo => statusItemInfo;
    [SerializeField] private CalculationOrder calculationOrder;
    public CalculationOrder CalculationOrder => calculationOrder;
    [SerializeField] private float scale;
    public float Scale => scale;
    [SerializeField] private float adjustment;
    public float Adjustment => adjustment;
}