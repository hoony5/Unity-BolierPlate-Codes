using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class CharacterCreateManager : MonoBehaviour
{
    [field:SerializeField] public CreateNpcAbility CreateNpcAbility { get; private set; }
    [field:SerializeField] public CreateNpcTraits CreateNpcTraits { get; private set; }
    private Dictionary<string, NPC> npcDataDictionary;

    public NPC GetNPCData(string npcName)
    {
        return npcDataDictionary.TryGetValue(npcName, out NPC npc) ? npc : null;
    }

    public void ClearNPCData()
    {
        npcDataDictionary.Clear();
    }
    
    public void Init()
    {
        List<NPC> npcs = CreateNpcAbility.SetNpcs();
        CreateNpcTraits.SetNpcAttributes(ref npcs);
        npcDataDictionary = npcs.ToDictionary(key => key.Name, value => value);
    }
}