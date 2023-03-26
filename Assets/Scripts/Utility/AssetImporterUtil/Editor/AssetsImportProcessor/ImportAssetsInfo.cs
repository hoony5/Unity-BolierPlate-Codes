using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ImportAssetsInfo", menuName = "ScriptableObject/Resource/ImportAssetsInfo", order = 1)]
public class ImportAssetsInfo : ScriptableObject
{
    [SerializeField] private PathInfo[] pathInfos;

    public string[] GetAllPaths()
    {
        return pathInfos.Select(i => i.rootPath + i.subPath).ToArray();
    }
    public PathInfo GetPathInfo(string assetName)
    {
        string lowerCaseAssetName = assetName.ToLower();

        foreach (PathInfo pathInfo in pathInfos)
        {
            if (pathInfo.keywords == null) continue;
            if (pathInfo.keywords.Any(keyword => lowerCaseAssetName.Contains(keyword.ToLower())))
            {
                return pathInfo;
            }
        }

        Debug.LogError($"No path info found for asset: {assetName}");
        return null;
    }
}