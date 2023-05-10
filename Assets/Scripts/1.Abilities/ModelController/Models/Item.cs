﻿using UnityEngine;

[System.Serializable]
public class Item : ModuleController
{
    [field: SerializeField] public string Tag { get; private set; }
    [field: SerializeField] public Transform Transform { get; private set; }
    [field: SerializeField] public bool IsQuestItem { get; private set; }
    [field: SerializeField] public int Count { get; private set; }
    [field: SerializeField] public int MaxCount { get; private set; }

    public bool CompareTag(string tag)
    {
        return !string.IsNullOrEmpty(tag) && !string.IsNullOrEmpty(Tag) && Tag == tag;
    }

}