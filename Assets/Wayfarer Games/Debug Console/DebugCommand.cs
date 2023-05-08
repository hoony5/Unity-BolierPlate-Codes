using System;
using System.Collections.Generic;
using UnityEngine;

namespace DebugConsole
{
    public abstract class DebugCommand : ScriptableObject
    {
        public string Name => name;

        [SerializeField] private string targetOptionsType = "BaseItem";
        public string TargetOptionsType => targetOptionsType;

        [SerializeField] private List<ScriptableObject> targets;
        public List<ScriptableObject> Targets => targets;

        [SerializeField] private int numParameters;
        public int NumParameters => numParameters;

        public abstract void Execute(GameObject target, string targetName, params string[] args);
    }
}