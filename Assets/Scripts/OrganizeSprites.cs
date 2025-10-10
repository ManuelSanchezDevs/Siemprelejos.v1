// 10/10/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;
using System.IO;

public class OrganizeSprites
{
    [MenuItem("Tools/Organize Animated Sprites")]
    public static void OrganizeAnimatedSprites()
    {
        string[] spritePaths = AssetDatabase.FindAssets("t:Sprite", new[] { "Assets/Animations" });
        string targetFolder = "Assets/OrganizedTiles";

        if (!AssetDatabase.IsValidFolder(targetFolder))
        {
            AssetDatabase.CreateFolder("Assets", "OrganizedTiles");
        }

        foreach (string guid in spritePaths)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            string fileName = Path.GetFileName(path);
            string destinationPath = Path.Combine(targetFolder, fileName);

            if (!File.Exists(destinationPath))
            {
                AssetDatabase.MoveAsset(path, destinationPath);
                Debug.Log($"Moved {fileName} to {targetFolder}");
            }
        }

        AssetDatabase.Refresh();
        Debug.Log("All animated sprites have been organized!");
    }
}
