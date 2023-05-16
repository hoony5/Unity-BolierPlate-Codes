using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ItemCreateManager : MonoBehaviour
{
    [field:SerializeField] public CreateItemAbility CreateItemAbility { get; private set; }
    [field:SerializeField] public CreateItemTraits CreateItemTraits { get; private set; }
    private Dictionary<string, StatusItem> statusItemDataDictionary;
    private Dictionary<string, Item> itemDataDictionary;

    public StatusItem GetStatusItemData(string itemName)
    {
        return statusItemDataDictionary.TryGetValue(itemName, out StatusItem item) ? item : null;
    }

    public void ClearStatusItemData()
    {
        statusItemDataDictionary.Clear();
    }
    
    public void Init()
    {
        List<StatusItem> statusItems = CreateItemAbility.SetStatusItems();
        CreateItemTraits.SetStatusItemAttributes(ref statusItems);
        statusItemDataDictionary = statusItems.ToDictionary(key => key.Name, value => value);
        
        List<Item> items = CreateItemTraits.LoadItems();
        itemDataDictionary = items.ToDictionary(key => key.Name, value => value);
    }
    
    public Item GetItemData(string itemName)
    {
        return itemDataDictionary.TryGetValue(itemName, out Item item) ? item : null;
    }

    public void ClearItemData()
    {
        statusItemDataDictionary.Clear();
    }
}