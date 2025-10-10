// 10/10/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;
using System.IO;

public class RemoveEmptyFolders
{
    [MenuItem("Tools/Remove Empty Folders")]
    public static void RemoveEmptyFoldersInProject()
    {
        string assetsPath = Application.dataPath;
        int removedFoldersCount = RemoveEmptyFoldersRecursively(assetsPath);

        AssetDatabase.Refresh();
        Debug.Log($"Proceso completado. Se eliminaron {removedFoldersCount} carpetas vacías.");
    }

    private static int RemoveEmptyFoldersRecursively(string folderPath)
    {
        int removedFoldersCount = 0;

        foreach (string subFolder in Directory.GetDirectories(folderPath))
        {
            removedFoldersCount += RemoveEmptyFoldersRecursively(subFolder);
        }

        // Si la carpeta está vacía después de procesar sus subcarpetas, elimínala
        if (Directory.GetFiles(folderPath).Length == 0 && Directory.GetDirectories(folderPath).Length == 0)
        {
            Directory.Delete(folderPath);
            string relativePath = "Assets" + folderPath.Substring(Application.dataPath.Length);
            AssetDatabase.DeleteAsset(relativePath);
            Debug.Log($"Eliminada carpeta vacía: {relativePath}");
            removedFoldersCount++;
        }

        return removedFoldersCount;
    }
}
