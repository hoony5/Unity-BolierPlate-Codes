using UnityEngine;

public class CharacterAbility : MonoBehaviour
{
    [SerializeField] private AllStatusInfos allStatusInfos;
    [SerializeField] private BuffStatusComponent buffStat ;
    [SerializeField] private DebuffStatusComponent debuffStat;
    [SerializeField] private OriginalStatusComponent originalStat;
    [SerializeField] private EquipmentStatusComponent equipmentStat;
    [SerializeField] private PetStatusComponent petStat;
    [SerializeField] private SkillStatusComponent skillStatus;
    
    public AllStatusInfos AllStatusInfos => allStatusInfos;
    public BuffStatusComponent BuffStat => buffStat;
    public DebuffStatusComponent DebuffStat => debuffStat;
    public OriginalStatusComponent OriginalStat => originalStat;
    public EquipmentStatusComponent EquipmentStat => equipmentStat;
    public PetStatusComponent PetStat => petStat;
    public SkillStatusComponent SkillStatus => skillStatus;
}
