using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using Object = UnityEngine.Object;

public class AssetsImportProcessor : AssetPostprocessor
{
    private static bool CheckExcludeExtensions(string name)
    {
        return name.Contains(".meta")
               || name.Contains(".cs")
               || name.Contains(".pdf")
               || name.Contains(".docx")
               || name.Contains(".txt")
               || name.Contains(".md")
               || name.Contains(".prefab")
               || name.Contains(".asset")
               || name.Contains(".dll")
               || name.Contains(".xlsx")
               || name.Contains(".csv")
               || (name.Contains(".asmdef") && !name.Contains("ImportAssetsInfo.asset"))
               || name.Equals(nameof(AssetsImportProcessor));
    }
   static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
{
    ImportAssetsInfo importAssetsInfo = AssetDatabase.LoadAssetAtPath<ImportAssetsInfo>("Assets/Resources/AssetImportSetting/ImportAssetsInfo.asset");
    if(importAssetsInfo is null) return;
    string[] importPaths = importAssetsInfo.GetAllPaths();

    // if any of the import paths doesn't exist, create it
    foreach (string path in importPaths)
    {
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
    }

    foreach (string importingAsset in importedAssets)
    {
        if (CheckExcludeExtensions(importingAsset)) continue;

        // import folder -> execute recursive import
        if (AssetDatabase.IsValidFolder(importingAsset))
        {
            ProcessFolder(importingAsset, importAssetsInfo);
            continue;
        }

        Object asset = AssetDatabase.LoadAssetAtPath(importingAsset, typeof(Object));
        // import asset file
        if (AssetDatabase.GetMainAssetTypeAtPath(importingAsset) != typeof(DefaultAsset))
        {
            ProcessAsset(importingAsset,asset, importAssetsInfo);
            continue;
        }

        // move asset to new Path
        PathInfo pathInfo = importAssetsInfo.GetPathInfo(asset.name);

        if (pathInfo is null)
        {
            Debug.LogWarning($"No path info found for asset: {asset.name}", asset);
            continue;
        }

        string assetPath = pathInfo.rootPath + pathInfo.subPath;
        string extension = Path.GetExtension(importingAsset);
        string assetName = $"{asset.name}{extension}";
        string newPath = assetPath + assetName;
        switch (extension)
        {
            // UI -> set settings. change texture type.
            case ".tga" or ".png" or ".jpg" or ".jpeg":
                UIAssetImport(importingAsset, newPath);
                break;
            // Model -> set settings. extract materials.
            case ".fbx":
            case ".obj":
                ModelAssetImport(importingAsset);
                break;
            default:
                AssetDatabase.MoveAsset(importingAsset, newPath);
                break;
        }
    }

    /*// if there is no subfolder and no asset in the folder, delete it
    foreach (string importingAsset in importedAssets)
    {
        if (AssetDatabase.IsValidFolder(importingAsset) && AssetDatabase.GetSubFolders(importingAsset).Length == 0 &&
            AssetDatabase.FindAssets("", new[] {importingAsset}).Length == 0)
        {
            AssetDatabase.DeleteAsset(importingAsset);
        }
    }*/
}

static void ProcessFolder(string folderPath, ImportAssetsInfo importAssetsInfo)
{
    string[] subFolders = AssetDatabase.GetSubFolders(folderPath);
    foreach (string subFolder in subFolders)
    {
        ProcessFolder(subFolder, importAssetsInfo);
    }

    Object[] subAssets = AssetDatabase.LoadAllAssetsAtPath(folderPath);
    foreach (Object subAsset in subAssets)
    {
        ProcessAsset(folderPath, subAsset, importAssetsInfo);
    }
}

static void ProcessAsset( string folderPath,Object asset, ImportAssetsInfo importAssetsInfo)
{
    if (asset is null) return;
    if (CheckExcludeExtensions(folderPath)) return;
     // Load PreCached-PathInfo.
    PathInfo pathInfo = importAssetsInfo.GetPathInfo(asset.name);

    if (pathInfo is null)
    {
        Debug.LogWarning($"No path info found for asset: {asset.name}", asset);
        return;
    }

    string assetPath = pathInfo.rootPath + pathInfo.subPath;
    string oldPath = AssetDatabase.GetAssetPath(asset);
    string extension = Path.GetExtension(oldPath);
    string assetName = $"{asset.name}{extension}";
    string newPath = assetPath + assetName;
    switch (extension)
    {
        // UI
        case ".tga" or ".png" or ".jpg" or ".jpeg":
            UIAssetImport(oldPath, newPath);
            break;
        // Model
        case ".fbx":
        case ".obj":
            ModelAssetImport(oldPath);
            break;
        default:
            if (pathInfo.keywords is not null)
            {
                if (pathInfo.keywords.Any(keyword => assetName.ToLower().Contains(keyword.ToLower())))
                    AssetDatabase.MoveAsset(oldPath, newPath);
            }
            else
                AssetDatabase.MoveAsset(oldPath, newPath);
            break;
    }
}
    private static void UIAssetImport(string path, string newPath)
    {
        TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
        string assetName = Path.GetFileNameWithoutExtension(path);
        string lowerCaseAssetName = assetName.ToLower();

        // If asset name contains "UI", change texture type to sprite
        if (lowerCaseAssetName.Contains("ui"))
        {
            textureImporter.textureType = TextureImporterType.Sprite;
            textureImporter.alphaIsTransparency = true;
        }

        textureImporter.SaveAndReimport();
        AssetDatabase.MoveAsset(path, newPath);
    }
    // fbx etc
    private static void ModelAssetImport(string path)
    {
        string assetName = Path.GetFileNameWithoutExtension(path);
        string extension = Path.GetExtension(path);
        string lowerCaseAssetName = assetName.ToLower();

        // Determine model and material paths based on asset name
        string modelPath = "";
        string materialPath = "";
        if (lowerCaseAssetName.Contains("character"))
        {
            modelPath = "Assets/Resources/Models/Character/";
            materialPath = "Assets/Resources/Materials/Character/";
        }
        else
        {
            modelPath = "Assets/Resources/Models/Environment/";
            materialPath = "Assets/Resources/Materials/Environment/";
        }

        // Import model
        ModelImporter modelImporter = AssetImporter.GetAtPath(path) as ModelImporter;
        modelImporter.animationType = ModelImporterAnimationType.None;
        modelImporter.materialLocation = ModelImporterMaterialLocation.External;
        modelImporter.SaveAndReimport();

        // Get resources in the model.
        Dictionary<AssetImporter.SourceAssetIdentifier, Object> externalObjectMap = modelImporter.GetExternalObjectMap();
        foreach (AssetImporter.SourceAssetIdentifier key in externalObjectMap.Keys)
        {
            // Extract materials
            if (externalObjectMap[key] is Material material)
            {
                string materialName = material.name;
                string materialAssetPath = materialPath + $"{materialName}.mat";
                AssetDatabase.CreateAsset(material, materialAssetPath);
            }

            // Extract Animators
            if (externalObjectMap[key] is Animator animator)
            {
                AnimatorController animatorController = (AnimatorController)animator.runtimeAnimatorController;
                if (animatorController == null) continue;
                string controllerAssetPath = modelPath + $"{animatorController.name}.controller";
                AnimatorController newController = new AnimatorController();
                AssetDatabase.CreateAsset(newController, controllerAssetPath);
                newController.name = animatorController.name;

                AnimatorControllerLayer[] layers = animatorController.layers;
                foreach (AnimatorControllerLayer layer in layers)
                {
                    AnimatorStateMachine stateMachine = layer.stateMachine;
                    newController.AddLayer(layer.name);
                    newController.layers[^1].stateMachine = stateMachine;
                }

                // Replace Animator Controller reference
                GameObject modelAsset = AssetDatabase.LoadAssetAtPath<GameObject>(modelPath + $"{assetName}{extension}");
                bool exist = modelAsset.TryGetComponent(out Animator ownedAnimator);
                if(!exist) modelAsset.AddComponent<Animator>();
                ownedAnimator.runtimeAnimatorController = newController;
                ownedAnimator.Update(0);
            }
        }
        // Move model file .obj or .fbx
        AssetDatabase.MoveAsset(path, modelPath + $"{assetName}{extension}");
    }
}