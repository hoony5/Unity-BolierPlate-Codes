using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using ArtificerPro.Item;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ArtificerPro.LootTable
{
    [CustomEditor(typeof(LootTable))]
    public class LootTableEditor : Editor
    {
        const string KListLayoutPath = "Assets/Wayfarer Games/Artificer Pro/Editor/Item/ItemList.uxml";
        private VisualElement _container;

        private SerializedProperty _list;
        private string _currentFilter;
        private void OnEnable()
        {
            _list = serializedObject.FindProperty("loot");
        }

        public override VisualElement CreateInspectorGUI()
        {
            var tree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(KListLayoutPath);
            if (tree == null)
                throw new FileNotFoundException("Couldn't find layout file, if you moved this asset please change KListLayoutPath to reflect your changes");

            _container = tree.Instantiate();
            var content = _container.Q<VisualElement>("Content");
            content.style.display = DisplayStyle.None;
            var button = _container.Q<Button>("Load");

            void ShowContent(ClickEvent evt)
            {
                content.style.display = DisplayStyle.Flex;
                button.style.display = DisplayStyle.None;
                var item = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(ItemUtils.KItemLayoutPath);
                if (item == null) throw new FileNotFoundException("Couldn't find layout file, if you moved this asset please change KItemLayoutPath to reflect your changes");

                var items = AssetDatabase.FindAssets($"t:{nameof(BaseItem)}")
                    .Select(AssetDatabase.GUIDToAssetPath)
                    .Select(AssetDatabase.LoadAssetAtPath<BaseItem>)
                    .ToList();

                var scrollview = _container.Q<ScrollView>("Items");

                var search = _container.Q<ToolbarSearchField>("Search");
                search.RegisterCallback<ChangeEvent<string>>(Search);

                var filter = _container.Q<ToolbarMenu>("Filter");
                filter.variant = ToolbarMenu.Variant.Popup;
                filter.menu.AppendAction("In this loot table", FilterAttached);
                filter.menu.AppendAction("All", FilterAll);

                var rarities = AssetDatabase.FindAssets($"t:{nameof(Rarity)}")
                    .Select(AssetDatabase.GUIDToAssetPath)
                    .Select(AssetDatabase.LoadAssetAtPath<Rarity>)
                    .OrderByDescending(r => r.Weight)
                    .ToList();

                foreach (var rarity in rarities)
                {
                    filter.menu.AppendAction(rarity.name, FilterRarity);

                    void FilterRarity(DropdownMenuAction action)
                    {
                        _currentFilter = rarity.name;
                        var all = scrollview.Query<VisualElement>("Container").ToList();

                        var found = all.Where(ve => ve.Q<DropdownField>("Rarity").value.ToLower().Contains(rarity.name.ToLower()))
                            .ToArray();
                        var hide = all.Where(ve => !ve.Q<DropdownField>("Rarity").value.ToLower().Contains(rarity.name.ToLower()))
                            .ToArray();

                        foreach (var item in found) item.style.display = DisplayStyle.Flex;
                        foreach (var item in hide) item.style.display = DisplayStyle.None;
                    }
                }

                foreach (var itemAsset in items)
                {
                    var instance = ItemUtils.CreateItem(scrollview.contentContainer, item, itemAsset, rarities);
                    var loot = GetLoot();

                    bool inLootTable = loot.Any(item => item.name == instance.Q<Label>("Name").text);

                    instance.style.backgroundColor = inLootTable ? new StyleColor(UnityEngine.Color.green) : new StyleColor(UnityEngine.Color.clear);
                    var asset = itemAsset;
                    
                    var button = new Button(OnClick)
                    {
                        text = inLootTable ? "Remove" : "Add",
                        name = "AddToItem"
                    };

                    
                    void OnClick()
                    {
                        loot = GetLoot();
                        inLootTable = loot.Any(item => item.name == instance.Q<Label>("Name").text);
                        if (inLootTable)
                        {
                            var loot = GetLoot();
                            loot.Remove(asset);
                            --_list.arraySize;
                            for (int i = 0; i < _list.arraySize; ++i) _list.GetArrayElementAtIndex(i).objectReferenceValue = loot[i];
                            Debug.Log("Removing item from loot table");
                        }
                        else
                        {
                            var current = _list.arraySize;
                            _list.InsertArrayElementAtIndex(current);
                            _list.GetArrayElementAtIndex(current).objectReferenceValue = asset;
                            Debug.Log("Adding item to loot table at position " + current);
                        }
                        instance.Q<VisualElement>("Container").Q<Button>("AddToItem").text = inLootTable ? "Add" : "Remove";
                        instance.style.backgroundColor = inLootTable ? new StyleColor(UnityEngine.Color.clear) : new StyleColor(UnityEngine.Color.green);
                        serializedObject.ApplyModifiedProperties();
                        if (_currentFilter == "attached")
                            ShowAttached();
                    }

                    instance.Q<VisualElement>("Container").Add(button);
                }
                
                if (_list.arraySize > 0)
                    ShowAttached();
            }

            button.RegisterCallback<ClickEvent>(ShowContent);
            return _container;
        }

        private void FilterAll(DropdownMenuAction action)
        {
            _currentFilter = "all";
            var scrollview = _container.Q<ScrollView>("Items");
            var all = scrollview.Query<VisualElement>("Container").ToList();
            foreach (var item in all)
                item.style.display = DisplayStyle.Flex;
        }

        private void FilterAttached(DropdownMenuAction action)
        {
            ShowAttached();
        }

        private List<BaseItem> GetLoot()
        {
            var loot = new List<BaseItem>();
            for (int i = 0; i < _list.arraySize; ++i)
            {
                if (_list.GetArrayElementAtIndex(i).objectReferenceValue is BaseItem item)
                    loot.Add(item);
            }

            return loot;
        }
        
        private void ShowAttached()
        {
            _currentFilter = "attached";
            var scrollview = _container.Q<ScrollView>("Items");
            var all = scrollview.Query<VisualElement>("Container").ToList();

            var loot = GetLoot();
            
            var found = all.Where(ve => loot.Any(item => item.name == ve.Q<Label>("Name").text))
                .ToArray();
            var hide = all.Where(ve => !loot.Any(item => item.name == ve.Q<Label>("Name").text))
                .ToArray();

            foreach (var item in found) item.style.display = DisplayStyle.Flex;
            foreach (var item in hide) item.style.display = DisplayStyle.None;
        }

        private void Search(ChangeEvent<string> search)
        {
            var scrollview = _container.Q<ScrollView>("Items");
            var all = scrollview.Query<VisualElement>("Container").ToList();
            var found = all.Where(ve => ve.Q<Label>("Name").text.ToLower().Contains(search.newValue.ToLower()))
                .ToArray();
            var hide = all.Where(ve => !ve.Q<Label>("Name").text.ToLower().Contains(search.newValue.ToLower()))
                .ToArray();

            foreach (var item in found)
                item.style.display = DisplayStyle.Flex;
            foreach (var item in hide)
                item.style.display = DisplayStyle.None;
        }

    }
}