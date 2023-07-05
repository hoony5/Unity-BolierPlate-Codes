using UnityEngine;
using System.Collections.Generic;

public static class ComponentContainer
{
    private static Dictionary<int, Container> _componentContainers = new Dictionary<int, Container>();

    public enum LoadMode
    {
        Self,
        Children,
        Parent
    }
    public static bool TryAddorUpdate(GameObject go, LoadMode mode = LoadMode.Self)
    {
        if (go is null) return false;

        int key = go.GetInstanceID();
        bool exist = _componentContainers.TryGetValue(key, out var container);
        
        Container value = exist ? container : new Container(go);
        
        if(exist) _componentContainers[key] = value;
        else _componentContainers.Add(key, value);

        switch (mode)
        {
            default:
            case LoadMode.Self:
                value.CacheComponents();
                break;
            case LoadMode.Children:
                value.CacheChildrenComponents();
                break;
            case LoadMode.Parent:
                value.CacheParentComponents();
                break;
        }

        return true;
    }
    
    public static bool ClearComponents(GameObject go)
    {
        if (go is null) return false;
        int key = go.GetInstanceID();
        if (!_componentContainers.TryGetValue(key, out Container container)) return false;
        container.Release();
        return true;
    }

    public static bool TryGetComponent<T>(this GameObject go, out T component) where T : Component
    {
        component = null;
        if(go is null) return false;
        int key = go.GetInstanceID();
        return _componentContainers.TryGetValue(key, out Container container) && container.TryGet(out component);
    }
    
    public static void TrimComponents()
    {
        foreach (KeyValuePair<int, Container> pair in _componentContainers)
        {
            Container container = pair.Value;
            if(container.RootObjectIsNull) _componentContainers.Remove(pair.Key);
        }
    }
}