using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[System.Serializable]
public class EffectDashBoard
{
    [field:SerializeField]public SerializedDictionary<string, ushort> PositiveBattleEffect {get; private set;}
    [field:SerializeField]public SerializedDictionary<string, ushort> NegativeBattleEffect {get; private set;}
    [field:SerializeField]public SerializedDictionary<string, ushort> PositiveGlobalEffect {get; private set;}
    [field:SerializeField]public SerializedDictionary<string, ushort> NegativeGlobalEffect {get;private set;}
    
    public void Init(int capacity = 32)
    {
        PositiveBattleEffect = new SerializedDictionary<string, ushort>(capacity);
        NegativeBattleEffect = new SerializedDictionary<string, ushort>(capacity);
        PositiveGlobalEffect = new SerializedDictionary<string, ushort>(capacity);
        NegativeGlobalEffect = new SerializedDictionary<string, ushort>(capacity);
    }

    public bool ExistPositiveBattleEffect(string effectName)
    {
        return PositiveBattleEffect.ContainsKey(effectName);
    }

    public bool ExistNegativeBattleEffect(string effectName)
    {
        return NegativeBattleEffect.ContainsKey(effectName);
    }

    public bool ExistPositiveGlobalEffect(string effectName)
    {
        return PositiveGlobalEffect.ContainsKey(effectName);
    }
    public bool ExistNegativeGlobalEffect(string effectName)
    {
        return NegativeGlobalEffect.ContainsKey(effectName);
    }

    public void AddGlobalEffect(EffectType effectType, string effectName)
    {
        switch (effectType)
        {
            case EffectType.Positive:
                PositiveGlobalEffect[effectName]++;
                break;
            case EffectType.Negative:
                NegativeGlobalEffect[effectName]++;
                break;
            case EffectType.None:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(effectType), effectType, null);
        }
    }

    public void AddBattleEffect(EffectType effectType, string effectName)
    {
        switch (effectType)
        {
            case EffectType.Positive:
                PositiveBattleEffect[effectName]++;
            break;
            case EffectType.Negative:
                NegativeBattleEffect[effectName]++;
            break;
            case EffectType.None:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(effectType), effectType, null);
        }
    }
    public void SubtractGlobalEffect(EffectType effectType, string effectName)
    {
        switch (effectType)
        {
            case EffectType.Positive:
                PositiveGlobalEffect[effectName]--;
                break;
            case EffectType.Negative:
                NegativeGlobalEffect[effectName]--;
                break;
            case EffectType.None:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(effectType), effectType, null);
        }
    }

    public void SubtractBattleEffect(EffectType effectType, string effectName)
    {
        switch (effectType)
        {
            case EffectType.Positive:
                PositiveBattleEffect[effectName]--;
            break;
            case EffectType.Negative:
                NegativeBattleEffect[effectName]--;
            break;
            case EffectType.None:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(effectType), effectType, null);
        }
    }
    public void ClearGlobalEffect(EffectType effectType, string effectName)
    {
        switch (effectType)
        {
            case EffectType.Positive:
                PositiveGlobalEffect[effectName] = 0;
                break;
            case EffectType.Negative:
                NegativeGlobalEffect[effectName] = 0;
                break;
            case EffectType.None:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(effectType), effectType, null);
        }
    }

    public void ClearBattleEffect(EffectType effectType, string effectName)
    {
        switch (effectType)
        {
            case EffectType.Positive:
                PositiveBattleEffect[effectName] = 0;
            break;
            case EffectType.Negative:
                NegativeBattleEffect[effectName] = 0;
            break;
            case EffectType.None:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(effectType), effectType, null);
        }
    }
}