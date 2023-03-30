using UnityEngine;

public class EffectValueInfo : MonoBehaviour
{
    [SerializeField] private string effectName;
    [SerializeField] private int level;
    [SerializeField] private int maxLevel;
    [SerializeField] private float[] baseValues;
    [SerializeField] private float[] coolTimes;
    [SerializeField] private float[] costs;

    public string EffectName => effectName;
    public int Level => level;
    public int MaxLevel => maxLevel;
    public float[] BaseValues => baseValues;
    public float[] CoolTimes => coolTimes;
    public float[] Costs => costs;

}