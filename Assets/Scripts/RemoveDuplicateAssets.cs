// 10/10/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class RemoveDuplicateAssets
{
    [MenuItem("Tools/Remove Duplicate Assets")]
    public static void RemoveDuplicates()
    {
        string[] assetGuids = AssetDatabase.FindAssets("t:Sprite");
        Dictionary<string, string> uniqueAssets = new Dictionary<string, string>();
        List<string> duplicates = new List<string>();

        foreach (string guid in assetGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            string fileName = Path.GetFileName(path);

            if (uniqueAssets.ContainsKey(fileName))
            {
                duplicates.Add(path);
            }
            else
            {
                uniqueAssets[fileName] = path;
            }
        }

        foreach (string duplicatePath in duplicates)
        {
            bool success = AssetDatabase.DeleteAsset(duplicatePath);
            if (success)
            {
                Debug.Log($"Eliminado duplicado: {duplicatePath}");
            }
            else
            {
                Debug.LogWarning($"No se pudo eliminar: {duplicatePath}");
            }
        }

        AssetDatabase.Refresh();
        Debug.Log($"Proceso completado. Se eliminaron {duplicates.Count} duplicados.");
    }
}
