using System.Collections.Generic;
using System.IO;
using System.Linq;
using ArtificerPro.LootTable;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace ArtificerPro.Item
{
    public static class ItemUtils
    {
        public const string KItemLayoutPath = "Assets/Wayfarer Games/Artificer Pro/Editor/Item/Item.uxml";
        public static VisualElement CreateItem(VisualElement container, VisualTreeAsset itemTree, BaseItem item, List<Rarity> rarities)
        {
            var instance = itemTree.Instantiate();
            container.Add(instance);
            
            var serializedObject = new SerializedObject(item);

            var name = instance.Q<Label>("Name");
            name.text = item.name;

            var icon = instance.Q<Button>("Icon");
            icon.style.backgroundImage = new StyleBackground(item.Icon);
            
            var iconPicker = icon.Q<ObjectField>("IconPicker");
            iconPicker.value = item.Icon;

            iconPicker.RegisterValueChangedCallback(IconChanged);

            void IconChanged(ChangeEvent<Object> e)
            {
                serializedObject.FindProperty("icon").objectReferenceValue = e.newValue;
                serializedObject.ApplyModifiedProperties();
                icon.style.backgroundImage = new StyleBackground(item.Icon);
                if (e.newValue == null)
                    EditorGUIUtility.SetIconForObject(item, null);
                else
                    EditorGUIUtility.SetIconForObject(item, (e.newValue as Sprite)?.texture);
                
                AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(item));
            }

            var script = instance.Q<PropertyField>("Script");
            script.BindProperty(serializedObject.FindProperty("m_Script"));
            script.Bind(serializedObject);
            script.SetEnabled(false);
            
            var obj = instance.Q<ObjectField>("Object");
            obj.objectType = typeof(BaseItem);
            obj.value = item;
            obj.SetEnabled(false);
            
            var clone = instance.Q<Button>("Clone");
            clone.RegisterCallback<ClickEvent>(CloneObject);

            void CloneObject(ClickEvent evt)
            {
                var path = EditorUtility.SaveFilePanel($"Clone {item.name}", AssetDatabase.GetAssetPath(item), $"{item.name} Clone.asset", "asset");
                path = path.Replace(Application.dataPath, "Assets");
                Debug.Log(path);
                var clonedItem = Object.Instantiate(item);
                AssetDatabase.CreateAsset(clonedItem, path);
            }
            
            var description = instance.Q<PropertyField>("Description");
            description.BindProperty(serializedObject.FindProperty("description"));
            description.Bind(serializedObject);
            
            var rarity = instance.Q<DropdownField>("Rarity");
            
            rarity.choices = rarities.Select(r => r.name).ToList();
            rarity.RegisterValueChangedCallback(RarityChanged);
            
            void RarityChanged(ChangeEvent<string> e)
            {
                var r = rarities.First(r => r.name == e.newValue);
                serializedObject.FindProperty("rarity").objectReferenceValue = r;
                serializedObject.ApplyModifiedProperties();
                instance.Q<VisualElement>("Container").style.borderBottomColor = new StyleColor(r.Color);
                rarity.style.borderBottomWidth = 2;
                rarity.style.borderBottomColor = new StyleColor(r.Color);
            }
            
            rarity.value = item.Rarity == null ? rarities.First().name : item.Rarity.name;

            var external = instance.Q<Toggle>("ExternalTrigger");
            external.BindProperty(serializedObject.FindProperty("externalTrigger"));
            external.Bind(serializedObject);
            
            var events = instance.Q<PropertyField>("Events");
            events.BindProperty(serializedObject.FindProperty("events"));
            events.Bind(serializedObject);

            var trigger = instance.Q<Toggle>("TriggerOnEquip");
            trigger.RegisterValueChangedCallback(TriggerChanged);

            void TriggerChanged(ChangeEvent<bool> e)
            {
                events.style.display = e.newValue? DisplayStyle.None : DisplayStyle.Flex;
            }
            
            trigger.BindProperty(serializedObject.FindProperty("triggerOnEquip"));
            trigger.Bind(serializedObject);
            
            var extraFields = instance.Q<VisualElement>("ExtraFields");
            
            var iterator = serializedObject.GetIterator();
            if (iterator.NextVisible(true))
            {
                do
                {
                    if (iterator.propertyPath.Contains("description") 
                        || iterator.propertyPath.Contains("icon") 
                        || iterator.propertyPath.Contains("maxStacks") 
                        || iterator.propertyPath.Contains("rarity") 
                        || iterator.propertyPath.Contains("triggerOnEquip") 
                        || iterator.propertyPath.Contains("events")
                        || iterator.propertyPath.Contains("externalTrigger")
                        || iterator.propertyPath == "m_Script")
                        continue;
                    var propertyField = new PropertyField(iterator.Copy()) { name = "PropertyField:" + iterator.propertyPath };
 
                    if (iterator.propertyPath == "m_Script" && serializedObject.targetObject != null)
                        propertyField.SetEnabled(value: false);
 
                    propertyField.Bind(serializedObject);
                    extraFields.Add(propertyField);
                }
                while (iterator.NextVisible(false));
            }

            return instance;
        }
    }
    
    public class ItemListEditorWindow : EditorWindow
    {
        const string KListLayoutPath = "Assets/Wayfarer Games/Artificer Pro/Editor/Item/ItemList.uxml";
        private VisualElement _container;

        [MenuItem("Tools/Artificer Pro/Item List")]
        public static void ShowWindow()
        {
            var wnd = GetWindow<ItemListEditorWindow>();
            wnd.titleContent = new GUIContent("Item List");
        }
        private void CreateGUI()
        {
            _container = rootVisualElement;
            var tree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(KListLayoutPath);
            if (tree == null)
                throw new FileNotFoundException("Couldn't find layout file, if you moved this asset please change KListLayoutPath to reflect your changes");

            _container.Add(tree.Instantiate());
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
                    ItemUtils.CreateItem(scrollview.contentContainer, item, itemAsset, rarities);
            }

            button.RegisterCallback<ClickEvent>(ShowContent);
        }

        private void FilterAll(DropdownMenuAction action)
        {;
            var scrollview = _container.Q<ScrollView>("Items");
            var all = scrollview.Query<VisualElement>("Container").ToList();
            foreach (var item in all)
                item.style.display = DisplayStyle.Flex;
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
