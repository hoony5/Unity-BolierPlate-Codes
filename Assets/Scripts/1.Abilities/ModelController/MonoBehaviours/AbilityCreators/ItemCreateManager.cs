using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ItemCreateManager : MonoBehaviour
{
    [field:SerializeField] public CreateItemAbility CreateItemAbility { get; private set; }
    [field:SerializeField] public CreateItemTraits CreateItemTraits { get; private set; }
    private Dictionary<string, Item> itemDataDictionary;

    public Item GetItemData(string itemName)
    {
        return itemDataDictionary.TryGetValue(itemName, out Item item) ? item : null;
    }

    public void ClearItemData()
    {
        itemDataDictionary.Clear();
    }
    
    public void Init()
    {
        List<Item> items = CreateItemAbility.SetItems();
        CreateItemTraits.SetItemAttributes(ref items);
        itemDataDictionary = items.ToDictionary(key => key.Name, value => value);
    }
}