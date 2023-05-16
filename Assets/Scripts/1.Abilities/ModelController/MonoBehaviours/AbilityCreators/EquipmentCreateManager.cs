using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class EquipmentCreateManager : MonoBehaviour
{
    [field:SerializeField] public CreateEquipmentAbility CreateEquipmentAbility { get; private set; }
    [field:SerializeField] public CreateItemTraits CreateEquipmentTraits { get; private set; }
    private Dictionary<string, Equipment> equipmentsDataDictionary = new Dictionary<string, Equipment>();

    private Dictionary<string, GrowableEquipment> growableEquipmentsDictionary =
        new Dictionary<string, GrowableEquipment>();

    private Dictionary<string, EnhancableEquipment> enhancableEquipmentsDictionary =
        new Dictionary<string, EnhancableEquipment>();

    private Dictionary<string, CombinableEquipment> combinableEquipmentsDictionary =
        new Dictionary<string, CombinableEquipment>();

    public Equipment GetEquipmentData(string equipmentName)
    {
        return equipmentsDataDictionary.TryGetValue(equipmentName, out Equipment equipment) ? equipment : null;
    }
    public GrowableEquipment GetGrowableEquipmentData(string equipmentName)
    {
        return growableEquipmentsDictionary.TryGetValue(equipmentName, out GrowableEquipment equipment) ? equipment : null;
    }
    public EnhancableEquipment GetEnhancableEquipmentData(string equipmentName)
    {
        return enhancableEquipmentsDictionary.TryGetValue(equipmentName, out EnhancableEquipment equipment) ? equipment : null;
    }
    public CombinableEquipment GetCombinableEquipmentData(string equipmentName)
    {
        return combinableEquipmentsDictionary.TryGetValue(equipmentName, out CombinableEquipment equipment) ? equipment : null;
    }

    public void ClearEquipmentData()
    {
        equipmentsDataDictionary.Clear();
        growableEquipmentsDictionary.Clear();
        enhancableEquipmentsDictionary.Clear();
        combinableEquipmentsDictionary.Clear();
    }

    public void Init()
    {
        InitEquipment();
        InitGrowableEquipment();
        InitEnhancableEquipment();
        InitCombinableEquipment();
    }

    private void InitEquipment()
    {
        List<Equipment> equipments = CreateEquipmentAbility.GetEquipments();
        if (equipments.Count == 0) return;
        CreateEquipmentTraits.SetEquipmentAttributes(ref equipments);
        equipmentsDataDictionary = equipments.ToDictionary(key => key.Name, value => value);
    }

    private void InitGrowableEquipment()
    {
        List<GrowableEquipment> equipments = CreateEquipmentAbility.GetGrowableEquipments();
        if (equipments.Count == 0) return;
        CreateEquipmentTraits.SetGrowInfo(ref equipments);
        growableEquipmentsDictionary = equipments.ToDictionary(key => key.Name, value => value);
    }

    private void InitEnhancableEquipment()
    {
        List<EnhancableEquipment> equipments = CreateEquipmentAbility.GetEnhancableEquipments();
        if (equipments.Count == 0) return;
        CreateEquipmentTraits.SetEnhanceInfo(ref equipments);
        enhancableEquipmentsDictionary = equipments.ToDictionary(key => key.Name, value => value);
    }
    private void InitCombinableEquipment()
    {
        List<CombinableEquipment> equipments = CreateEquipmentAbility.GetCombinableEquipments();
        if (equipments.Count == 0) return;
        combinableEquipmentsDictionary = equipments.ToDictionary(key => key.Name, value => value);
    }
}