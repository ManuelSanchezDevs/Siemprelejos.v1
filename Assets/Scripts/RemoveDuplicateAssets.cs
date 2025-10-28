// 10/10/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class RemoveDuplicateAssets : EditorWindow
{
    private string targetFolder = "Assets/Animations"; // Cambia esto si tu carpeta tiene otra ruta

    [MenuItem("Tools/Remove Duplicate Assets")]
    public static void ShowWindow()
    {
        GetWindow<RemoveDuplicateAssets>("Remove Duplicate Assets");
    }

    private void OnGUI()
    {
        GUILayout.Label("Remove Duplicate Assets", EditorStyles.boldLabel);

        targetFolder = EditorGUILayout.TextField("Target Folder", targetFolder);

        if (GUILayout.Button("Find and Remove Duplicates"))
        {
            RemoveDuplicates();
        }
    }

    private void RemoveDuplicates()
    {
        if (!AssetDatabase.IsValidFolder(targetFolder))
        {
            Debug.LogError($"The folder {targetFolder} does not exist.");
            return;
        }

        string[] allAssets = Directory.GetFiles(targetFolder, "*.*", SearchOption.AllDirectories);
        Dictionary<string, string> fileHashes = new Dictionary<string, string>();
        List<string> duplicates = new List<string>();

        foreach (string filePath in allAssets)
        {
            if (filePath.EndsWith(".meta")) continue; // Ignorar archivos .meta

            string fileName = Path.GetFileName(filePath);
            string fileHash = GetFileHash(filePath);

            if (fileHashes.ContainsKey(fileHash))
            {
                duplicates.Add(filePath);
                Debug.Log($"Duplicate found: {filePath} (Original: {fileHashes[fileHash]})");
            }
            else
            {
                fileHashes[fileHash] = filePath;
            }
        }

        foreach (string duplicate in duplicates)
        {
            File.Delete(duplicate);
            File.Delete(duplicate + ".meta"); // También eliminar el archivo .meta
            Debug.Log($"Deleted duplicate: {duplicate}");
        }

        AssetDatabase.Refresh();
        Debug.Log("Finished removing duplicates!");
    }

    private string GetFileHash(string filePath)
    {
        byte[] fileBytes = File.ReadAllBytes(filePath);
        return System.BitConverter.ToString(System.Security.Cryptography.MD5.Create().ComputeHash(fileBytes));
    }
}
