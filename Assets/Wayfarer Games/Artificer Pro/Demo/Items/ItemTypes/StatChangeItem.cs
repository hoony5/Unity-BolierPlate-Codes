using System;
using System.Collections;
using ArtificerPro.Events;
using ArtificerPro.Item;
using ArtificerPro.Stats;
using UnityEngine;
using Utils;

namespace ArtificerPro.Demo.Items.ItemTypes
{
    [CreateAssetMenu(fileName = "Item", menuName = "Artificer Pro/Create Stat Change Item")]
    public class StatChangeItem : BaseItem
    {
        [SerializeField] private Stat[] statsToChange;
        [SerializeField] private StatModifier[] modifiers;

        [SerializeField, Tooltip("0 for permanent stat change")]
        private float timeInSeconds;

        [SerializeField, Tooltip("0 for infinite")] 
        private int maxTempStacks = 1;

        [NonSerialized] private int _currentTempStacks;
        [NonSerialized] private GameObject[] _targets;

        private bool _unequipped;
        
        public override void DoEffect(TriggerEventArgs args)
        {
            Runner.Run(ChangeAfterTime(args.Target));
        }

        public override void EquipItem()
        {
            base.EquipItem();
            _unequipped = false;
            _currentTempStacks = 0;
        }

        public override void UnEquipItem()
        {
            base.UnEquipItem();
            _unequipped = true;
            _currentTempStacks = 0;
        }

        private IEnumerator ChangeAfterTime(GameObject target)
        {
            if (maxTempStacks > 0 && _currentTempStacks >= maxTempStacks) yield break;

            for (int i = 0; i < statsToChange.Length; i++)
            {
                // repeat for every item duplicate
                for (int j = 0; j < CurrentStacks; ++j)
                    statsToChange[i].AddModifier(modifiers[i], target);
            }
            
            ++_currentTempStacks;

            if (Mathf.Approximately(timeInSeconds, 0f))
                yield break;

            var timeLeft = timeInSeconds;
            
            // if unequipped, immediately remove the stat modifiers
            while (timeLeft > 0f && !_unequipped)
            {
                timeLeft -= Time.deltaTime;
                yield return null;
            }

            for (int i = 0; i < statsToChange.Length; i++)
            {
                // repeat for every item duplicate
                for (int j = 0; j < CurrentStacks; ++j)
                    statsToChange[i].RemoveModifier(modifiers[i], target);
            }
            --_currentTempStacks;
        }
    }
}