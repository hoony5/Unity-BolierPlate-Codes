using System;
using UnityEngine;

namespace ArtificerPro.Events
{
    public class TriggerEventArgs
    {
        public GameObject Sender;
        public GameObject Target;
        public object Data;

        public TriggerEventArgs(GameObject sender, GameObject target, object data)
        {
            Sender = sender;
            Target = target;
            Data = data;
        }
    }
    
    [CreateAssetMenu(menuName = "Artificer Pro/Create Item Trigger", fileName = "ItemEvent")]
    public class TriggerItemEvent : ScriptableObject
    {
        public event Action<TriggerEventArgs> OnEvent;
        
        public void Invoke(TriggerEventArgs args)
        {
            OnEvent?.Invoke(args);
        }
    }
}