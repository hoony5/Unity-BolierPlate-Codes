using System;
using UnityEngine;

namespace ArtificerPro.Stats
{
    public enum ModifierType { Multiply, Add }
    /// <summary>
    /// A serializable class use to modify stats of type Type
    /// </summary>
    [Serializable]
    public class StatModifier
    {
        public ModifierType Type;
        
        // a multiplier for the stat type. 
        public float MultiplyBy;
        
        // a base value for the change
        public float Add;
    }
}