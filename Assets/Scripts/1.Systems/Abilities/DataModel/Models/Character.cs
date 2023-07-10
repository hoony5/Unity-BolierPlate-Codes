using UnityEngine;

[System.Serializable]
public class Character : ModuleController, IGrowable
{
    [field: SerializeField] public int Level { get;  set; }
    [field: SerializeField] public int MaxLevel { get;  set; }
    [field: SerializeField] public int CurrentExp { get;  set; }
    [field: SerializeField] public int MaxExp { get;  set; }
    [field:SerializeField] public int CurrentEnergy { get;  set; }
    [field: SerializeField] public Transform Transform { get;  set; }
    [field: SerializeField] public CharacterType CharacterType { get;  set; }
    [field: SerializeField] public Status StatusAbility { get;  set; }

    [field: SerializeField] public PlayerAttributes Attributes { get;  set; }
    [field: SerializeField] public CharacterBehaviour Behaviour { get;  set; }

    public Character()
    {
        StatusAbility = new Status();
        Behaviour = new CharacterBehaviour();
    }
    public void ResetLevel() => Level = 0;

    public void ResetExp() => CurrentExp = 0;

    public void AddLevel(int level)
    {
        Level += level;
        if(Level > MaxLevel) Level = MaxLevel;
        if(Level < 0) Level = 0;
    }

    public bool TryAddExp(int exp)
    {
        CurrentExp += exp;
        if (CurrentExp >= MaxExp)
        {
            CurrentExp = MaxExp;
            return true;
        }
        if(CurrentExp < 0) CurrentExp = 0;
        return false;
    }

    public void LevelUp()
    {
        AddLevel(1);
        ResetExp();
    }

    public void SetBaseInfo(int maxLevel, int maxExp)
    {
        MaxLevel = maxLevel;
        MaxExp = maxExp;
    }
    public void SetAttributes(PlayerAttributes attributes)
    {
        Attributes = attributes;
    }

    public bool TryGetStatusAbility(string statusType, out StatusBaseAbility statusBaseAbility)
    {
        bool exist = StatusAbility
            .AbilityInfo
            .TryGetStatusAbility(statusType, out statusBaseAbility);
        if (!exist)
        {
            Debug.LogError($"{statusType} is not in Status");
            return false;
        }

        return true;
    }
}