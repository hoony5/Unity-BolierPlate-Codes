using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class EquipmentCreateManager : MonoBehaviour
{
    [field:SerializeField] public CreateEquipmentAbility CreateEquipmentAbility { get; private set; }
    [field:SerializeField] public CreateItemTraits CreateEquipmentTraits { get; private set; }
    private Dictionary<string, Equipment> equipmentsDataDictionary;

    public Equipment GetEquipmentData(string equipmentName)
    {
        return equipmentsDataDictionary.TryGetValue(equipmentName, out Equipment equipment) ? equipment : null;
    }

    public void ClearEquipmentData()
    {
        equipmentsDataDictionary.Clear();
    }
    
    public void Init()
    {
        List<Equipment> equipments = CreateEquipmentAbility.SetEquipments();
        CreateEquipmentTraits.SetEquipmentAttributes(ref equipments);
        equipmentsDataDictionary = equipments.ToDictionary(key => key.Name, value => value);
    }
}