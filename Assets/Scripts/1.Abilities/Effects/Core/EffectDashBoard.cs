using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[ToDo("이곳은 UI에서 보여지는 Effect 정보를 관리하는 공간 - 총 26개")]
public class EffectDashBoard : MonoBehaviour
{
    public List<string> PositiveBattleEffect {get; private set;}
    public List<string> NegativeBattleEffect {get; private set;}
    public List<string> PositiveGlobalEffect {get; private set;}
    public List<string> NegativeGlobalEffect {get;private set;}
    
    private void Init(int capacity = 32)
    {
        PositiveBattleEffect = new List<string>(capacity);
        NegativeBattleEffect = new List<string>(capacity);
        PositiveGlobalEffect = new List<string>(capacity);
        NegativeGlobalEffect = new List<string>(capacity);
    }

    private void Start()
    {
        Init();
    }

    public void AddGlobalEffect(EffectType effectType, string effectName)
    {
        switch (effectType)
        {
            case EffectType.Positive when !PositiveGlobalEffect.Contains(effectName):
                PositiveGlobalEffect.Add(effectName);
                break;
            case EffectType.Negative when !NegativeGlobalEffect.Contains(effectName):
                NegativeGlobalEffect.Add(effectName);
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
            case EffectType.Positive when !PositiveBattleEffect.Contains(effectName):
                PositiveBattleEffect.Add(effectName);
            break;
            case EffectType.Negative when !NegativeBattleEffect.Contains(effectName):
                NegativeBattleEffect.Add(effectName);
            break;
            case EffectType.None:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(effectType), effectType, null);
        }
    }
    public void RemoveGlobalEffect(EffectType effectType, string effectName)
    {
        switch (effectType)
        {
            case EffectType.Positive when PositiveGlobalEffect.Contains(effectName):
                PositiveGlobalEffect.Remove(effectName);
                break;
            case EffectType.Negative when NegativeGlobalEffect.Contains(effectName):
                NegativeGlobalEffect.Remove(effectName);
                break;
            case EffectType.None:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(effectType), effectType, null);
        }
    }

    public void RemoveBattleEffect(EffectType effectType, string effectName)
    {
        switch (effectType)
        {
            case EffectType.Positive when PositiveBattleEffect.Contains(effectName):
                PositiveBattleEffect.Remove(effectName);
            break;
            case EffectType.Negative when NegativeBattleEffect.Contains(effectName):
                NegativeBattleEffect.Remove(effectName);
            break;
            case EffectType.None:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(effectType), effectType, null);
        }
    }
    public void ClearGlobalEffect(EffectType effectType)
    {
        switch (effectType)
        {
            case EffectType.Positive:
                PositiveGlobalEffect.Clear();
                break;
            case EffectType.Negative:
                NegativeGlobalEffect.Clear();
                break;
            case EffectType.None:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(effectType), effectType, null);
        }
    }

    public void ClearBattleEffect(EffectType effectType)
    {
        switch (effectType)
        {
            case EffectType.Positive:
                PositiveBattleEffect.Clear();
            break;
            case EffectType.Negative:
                NegativeBattleEffect.Clear();
            break;
            case EffectType.None:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(effectType), effectType, null);
        }
    }
}