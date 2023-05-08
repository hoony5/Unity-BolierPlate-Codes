using System.Collections.Generic;
using ArtificerPro.Events;
using ArtificerPro.Item;
using UnityEngine;
using WayfarerGames.Common;

namespace ArtificerPro.Inventory
{
    public class EquippedItems : MonoBehaviour
    {
        [SerializeField, Tooltip("When an item is equipped, broadcast this event. Useful for updating UI")] 
        private BaseItemEvent onEquipItem;
        
        [SerializeField, Tooltip("When an item is unequipped, broadcast this event. Useful for updating UI")] 
        private BaseItemEvent onUnequipItem;

        [SerializeField, Tooltip("When an item cannot be equipped, broadcast this event. Useful for UI")] 
        private ScriptableEvent onEquipFailed;

        [SerializeField, Tooltip("Trigger item actions that listen for the equip event")] 
        private TriggerItemEvent triggerItemsEquip;
        
        [SerializeField, Tooltip("Trigger item actions that listen for the unequip event")]
        private TriggerItemEvent triggerItemsUnequip;
        
        private readonly List<BaseItem> _items = new ();
        public List<BaseItem> Items => _items;
        
        public void AddItem(BaseItem item)
        {
            if (item.CheckCanEquipItem())
            {
                item.EquipItem();
                _items.Add(item);
                onEquipItem.Invoke(item);

                var eventArgs = new TriggerEventArgs(gameObject, gameObject, item);
                
                triggerItemsEquip.Invoke(eventArgs);
                if (item.TriggerOnEquip)
                    item.DoEffect(eventArgs);
            } else 
                onEquipFailed.Invoke();
        }

        public void RemoveItem(BaseItem item)
        {
            item.UnEquipItem();
            _items.Remove(item);
            onUnequipItem.Invoke(item);
            triggerItemsUnequip.Invoke(new TriggerEventArgs (gameObject, gameObject, item));
        }

        public int ClearItems()
        {
            var count = _items.Count;

            foreach (var item in _items)
            {
                onUnequipItem.Invoke(item);
                triggerItemsUnequip.Invoke(new TriggerEventArgs (gameObject, gameObject, item));
            }
            
            _items.Clear();
            return count;
        }
    }
}