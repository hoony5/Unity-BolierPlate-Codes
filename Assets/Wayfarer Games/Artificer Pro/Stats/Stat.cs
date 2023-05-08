using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ArtificerPro.Stats
{
    [CreateAssetMenu (menuName = "Artificer Pro/Stat")]
    public class Stat : ScriptableObject
    {
        public event Action<float, float> OnUpdated;
        
        // set an initial value for this object
        [SerializeField] private float initialValue;

        // an easy way to make sure the numbers never get too big or small! 
        [SerializeField] private float maxValue = 1000;
        [SerializeField] private float minValue = -1000;

        // a private list of modifiers used to temporarily change the stat
        [NonSerialized] private Dictionary<GameObject, List<StatModifier>> _modifiers = new ();

        // the runtime value needs to not be serialized by Unity
        // fixes some bugs when testing in the Unity Editor
        [NonSerialized] private float? _runtimeValue;

        /// <summary>
        /// Get a stat, with any modifiers
        /// </summary>
        /// <param name="instance">The specific object instance to use</param>
        /// <returns></returns>
        public float GetValue(GameObject instance)
        {
            _runtimeValue ??= initialValue;
            
            var modifiedValue = _runtimeValue.Value;

            if (!_modifiers.ContainsKey(instance))
                _modifiers.Add(instance, new List<StatModifier>());
            
            var modifiers = _modifiers[instance];
            modifiedValue *= modifiers
                .Where(mod => mod.Type == ModifierType.Multiply)
                // multiply them all together
                // e.g. if we had a list of modifiers with values 0.5, 0.25, 1.5, we want to multiply the stat by 0.1875
                .Aggregate(1f, (val, mod) => val * mod.MultiplyBy);

            modifiedValue += modifiers
                .Where(mod => mod.Type == ModifierType.Add)
                .Sum(mod => mod.Add);
            
            return Mathf.Clamp(modifiedValue, minValue, maxValue);
        }
        
        /// <summary>
        /// Add a modifier to the list
        /// </summary>
        /// <param name="instance">The specific object instance to use</param>
        /// <param name="modifier"></param>
        public void AddModifier(StatModifier modifier, GameObject instance)
        {
            if (!_modifiers.ContainsKey(instance))
                _modifiers.Add(instance, new List<StatModifier>());

            float previous = GetValue(instance);
            _modifiers[instance].Add(modifier);
            OnUpdated?.Invoke(GetValue(instance), previous);
        }

        /// <summary>
        /// Remove a modifier from the list
        /// </summary>
        /// <param name="instance">The specific object instance to use</param>
        /// <param name="modifier"></param>
        public void RemoveModifier(StatModifier modifier, GameObject instance)
        {
            if (!_modifiers.ContainsKey(instance))
                _modifiers.Add(instance, new List<StatModifier>());
            
            float previous = GetValue(instance);
            _modifiers[instance].Remove(modifier);
            OnUpdated?.Invoke(GetValue(instance), previous);
        }

        /// <summary>
        /// Set the value of this stat
        /// </summary>
        /// <param name="val"></param>
        public void SetValue(float val)
        {
            _runtimeValue = val;
        }
    }
}
