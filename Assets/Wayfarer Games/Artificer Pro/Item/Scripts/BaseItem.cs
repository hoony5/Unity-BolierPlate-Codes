using System;
using ArtificerPro.Events;
using ArtificerPro.LootTable;
using UnityEngine;
using WayfarerGames.Common;

namespace ArtificerPro.Item
{
    public abstract class BaseItem : ScriptableObject, IWeightedItem
    {
        [SerializeField, TextArea, Tooltip("The description for the object, can be used by any UI that displays the item")] 
        protected string description;
        public string Description => description;

        [SerializeField, Tooltip("The sprite icon for the object, can be used by any UI that displays the item")] 
        protected Sprite icon;
        public Sprite Icon => icon;

        [SerializeField, Tooltip("The maximum number of times this object can be added to the inventory, -1 for infinite times")] 
        protected int maxStacks = 1;
        public int MaxStacks => maxStacks;

        [SerializeField, Tooltip("How often the item spawns, change rarity weights in Artificer Pro settings")] 
        protected Rarity rarity;
        public Rarity Rarity => rarity;

        [SerializeField, Tooltip("If true, the item's effect will happen immediately when picked up. Otherwise, use an event")] 
        private bool triggerOnEquip;
        public bool TriggerOnEquip => triggerOnEquip;
        
        [SerializeField, Tooltip("Which events will trigger this item")]
        protected TriggerItemEvent[] events;
        public TriggerItemEvent[] Events => events;
        
        [SerializeField, Tooltip("Can this item get triggered by other items?")]
        protected bool externalTrigger = true;
        public bool ExternalTrigger => externalTrigger;

        [NonSerialized] protected int _currentStacks = 0;
        public int CurrentStacks => _currentStacks;

        public bool CheckCanEquipItem()
        {
            // -1 denotes infinite stacks
            if (maxStacks == -1)
                return true;
            
            // max stacks is 0, so we can't equip it
            if (maxStacks == 0)
            {
                Debug.LogWarning("This item cannot be equipped - maxStacks is 0");
                return false;
            }
            
            // we have too many stacks, this item can't be equipped
            if (CurrentStacks >= maxStacks)
                return false;

            return true;
        }
        
        public virtual void EquipItem()
        {
            if (_currentStacks == 0)
            {
                foreach (var e in events)
                {
                    e.OnEvent += DoEffect;
                }
            }
            
            ++_currentStacks;
        }
        public virtual void UnEquipItem()
        {
            // increase stacks before the events are unsubscribed from, so any listeners get the updated stacks
            --_currentStacks;
            if (_currentStacks == 0)
            {
                foreach (var e in events)
                {
                    e.OnEvent -= DoEffect;
                }
            }
        }

        public abstract void DoEffect(TriggerEventArgs args);

        public float Weight => rarity.Weight;
    }
}
