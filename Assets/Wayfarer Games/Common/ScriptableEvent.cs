using System;
using UnityEngine;

namespace WayfarerGames.Common
{
    [CreateAssetMenu(fileName = "Scriptable Event", menuName = "Wayfarer Games/Scriptable Event")]
    public class ScriptableEvent : ScriptableObject
    {
        public event Action OnEvent;

        public void Invoke()
        {
            OnEvent?.Invoke();
        }
    }
    
    public abstract class ScriptableEvent<T> : ScriptableObject
    {
        public event Action<T> OnEvent;

        public void Invoke(T val)
        {
            OnEvent?.Invoke(val);
        }
    }
}