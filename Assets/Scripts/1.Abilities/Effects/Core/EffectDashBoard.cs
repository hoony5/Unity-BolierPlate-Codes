using System.Collections.Generic;

[System.Serializable]
public class EffectDashBoard
{
    public List<string> PositiveBattleEffect {get; private set;}
    public List<string> NegativeBattleEffect {get; private set;}
    public List<string> PositiveGlobalEffect {get; private set;}
    public List<string> NegativeGlobalEffect {get;private set;}
    
    public EffectDashBoard(int capacity = 32)
    {
        PositiveBattleEffect = new List<string>(capacity);
        NegativeBattleEffect = new List<string>(capacity);
        PositiveGlobalEffect = new List<string>(capacity);
        NegativeGlobalEffect = new List<string>(capacity);
    }
    
    
}