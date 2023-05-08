using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterAbility
{
    [field:SerializeField] public AllStatusInfos AllStatusInfos{get; private set;}
    [field:SerializeField] public BuffStatusComponent BuffStat {get; private set;}
    [field:SerializeField] public DebuffStatusComponent DebuffStat{get; private set;}
    [field:SerializeField] public OriginalStatusComponent OriginalStat{get; private set;}
    [field:SerializeField] public EquipmentStatusComponent EquipmentStat{get; private set;}
    [field:SerializeField] public PetStatusComponent PetStat{get; private set;}
    [field:SerializeField] public SkillStatusComponent SkillStatus{get; private set;}
    [field:SerializeField] public MotivationStatusComponent MotivationStatus{get; private set;}

    public CharacterAbility(List<StatusItemInfo> statusBaseInfo)
    {
        BuffStat = new BuffStatusComponent();
        BuffStat.AddStatusesBaseInfo(statusBaseInfo);
        DebuffStat = new DebuffStatusComponent();
        DebuffStat.AddStatusesBaseInfo(statusBaseInfo);
        OriginalStat = new OriginalStatusComponent();
        OriginalStat.AddStatusesBaseInfo(statusBaseInfo);
        EquipmentStat = new EquipmentStatusComponent();
        EquipmentStat.AddStatusesBaseInfo(statusBaseInfo);
        PetStat = new PetStatusComponent();
        PetStat.AddStatusesBaseInfo(statusBaseInfo);
        SkillStatus = new SkillStatusComponent();
        SkillStatus.AddStatusesBaseInfo(statusBaseInfo);
        MotivationStatus = new MotivationStatusComponent();
        MotivationStatus.AddStatusesBaseInfo(statusBaseInfo);
    }
    public void SetOriginalStatusComponent(OriginalStatusComponent originalStatusComponent)
    {
        OriginalStat = originalStatusComponent;
    }
}
