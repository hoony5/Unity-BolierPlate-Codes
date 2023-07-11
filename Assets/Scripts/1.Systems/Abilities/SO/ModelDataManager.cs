using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

// structure is => DataManager (core) => ModelDataManager + ReferenceDataManager
//              => ModelDataManager (Center) => Cache ModelSO
//             => ReferenceDataManager (Sub) => Cache AbilitySO & BattleBehaviourSO 
//             => SOCreator (Factory) => ModelSO = AbilitySO + BattleBehaviourSO or ModelSO = BattleBehaviourSO or ModelSO = AbilitySO
[Serializable]
public class ModelDataManager : MonoBehaviour
{
    [field: SerializeField] private SerializedDictionary<string, BattleBehaviourSO> BattleBehaviourSos { get; set; }
    [field: SerializeField] private SerializedDictionary<string, UsableStatusItemSO> UsableStatusItemSos { get; set; }
    [field: SerializeField] private SerializedDictionary<string, CombinableAchievementSO> CombinableAchievementSos { get; set; }
    [field: SerializeField] private SerializedDictionary<string, CombinableEquipmentSO> CombinableEquipmentSos { get; set; }
    [field: SerializeField] private SerializedDictionary<string, CombinablePetSO> CombinablePetSos { get; set; }
    [field: SerializeField] private SerializedDictionary<string, CombinableStatusItemSO> CombinableStatusItemSos { get; set; }
    [field: SerializeField] private SerializedDictionary<string, EnhancableAchievementSO> EnhancableAchievementSos { get; set; }
    [field: SerializeField] private SerializedDictionary<string, EnhancableEquipmentSO> EnhancableEquipmentSos { get; set; }
    [field: SerializeField] private SerializedDictionary<string, EnhancablePetSO> EnhancablePetSos { get; set; }
    [field: SerializeField] private SerializedDictionary<string, GrowableAchievementSO> GrowableAchievementSos { get; set; }
    [field: SerializeField] private SerializedDictionary<string, GrowableEquipmentSO> GrowableEquipmentSos { get; set; }
    [field: SerializeField] private SerializedDictionary<string, GrowablePetSO> GrowablePetSos { get; set; }
    [field: SerializeField] private SerializedDictionary<string, NPCSO> NpcSos { get; set; }
}