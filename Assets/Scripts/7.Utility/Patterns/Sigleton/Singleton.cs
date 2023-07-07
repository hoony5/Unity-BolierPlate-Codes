using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    [field:NonSerialized] private static T _Instance { get; set; }
    [field: NonSerialized] private static object _LockObject { get; } = new object();
    [field: SerializeField] public bool IsAliveAfterUnloadScene { get; set; } = true;
    
    public event UnityAction<T> OnWakeup; 
    public event UnityAction<T> OnRelease;
    
    public static T Instance
    {
        get
        {
            if (_Instance is not null) return _Instance;

            lock(_LockObject)
            {
                GameObject go = new GameObject($"{typeof(T).Name} Singleton Instance");
                _Instance = go.AddComponent<T>();

                if (go.GetComponent<Singleton<T>>().IsAliveAfterUnloadScene)
                    DontDestroyOnLoad(go);
            }
            
            return _Instance;
        }
    }

    protected virtual void OnEnable()
    {
        if (_Instance is null) return;
        OnWakeup?.Invoke(_Instance);
    }
    protected virtual void OnDisable()
    {
        Release();
    }

    private void Release()
    {
        OnRelease?.Invoke(_Instance);
        if (!_Instance || IsAliveAfterUnloadScene) return;
        DestroyImmediate(_Instance.gameObject);
    }
}