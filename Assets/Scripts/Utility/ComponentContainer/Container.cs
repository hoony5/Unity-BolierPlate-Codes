using UnityEngine;
using System;
using System.Collections.Generic;

public class Container
{
    private readonly GameObject gameObject;
    private readonly Dictionary<Type, Component> _components;

    public bool RootObjectIsNull => !gameObject;
    public Container(GameObject gameObject)
    {
        this.gameObject = gameObject;
        _components = new Dictionary<Type, Component>();
    }

    public void CacheComponents()
    {
        if (gameObject == null)
        {
            Debug.LogError("GameObject is null");
            return;
        }

        Component[] componentsBuffer = gameObject.GetComponents<Component>();
        foreach (Component component in componentsBuffer)
        {
            _ = _components.TryAdd(component.GetType(), component);
        }
    }

    public void CacheChildrenComponents(bool includeInactive = false)
    {
        if (gameObject == null)
        {
            Debug.LogError("GameObject is null");
            return;
        }

        Component[] componentsBuffer = gameObject.GetComponentsInChildren<Component>(includeInactive);
        foreach (Component component in componentsBuffer)
        {
            _ = _components.TryAdd(component.GetType(), component);
        }
    }
    
    public void CacheParentComponents(bool includeInactive = false)
    {
        if (gameObject == null)
        {
            Debug.LogError("GameObject is null");
            return;
        }

        Component[] componentsBuffer = gameObject.GetComponentsInParent<Component>(includeInactive);
        foreach (Component component in componentsBuffer)
        {
            _ = _components.TryAdd(component.GetType(), component);
        }
    }

    public bool Release()
    {
        if (gameObject is null) return false; 
        _components.Clear();

        return true;
    }

    public bool TryGet<T>(out T component) where T : Component
    {
        if (_components.TryGetValue(typeof(T), out Component result))
        {
            component = (T)result;
            return true;
        }

        component = null;
        return false;
    }
}