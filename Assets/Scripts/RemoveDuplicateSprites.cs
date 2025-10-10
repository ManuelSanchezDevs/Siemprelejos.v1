// 10/10/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class RemoveDuplicateSprites
{
    [MenuItem("Tools/Remove Duplicate Sprites")]
    public static void RemoveDuplicates()
    {
        string[] spriteGuids = AssetDatabase.FindAssets("t:Sprite");
        Dictionary<string, string> uniqueSprites = new Dictionary<string, string>();
        List<string> duplicates = new List<string>();

        foreach (string guid in spriteGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);

            if (sprite != null)
            {
                // Generar una clave única basada en las propiedades del sprite
                string uniqueKey = $"{sprite.texture.name}_{sprite.rect.x}_{sprite.rect.y}_{sprite.rect.width}_{sprite.rect.height}";

                if (uniqueSprites.ContainsKey(uniqueKey))
                {
                    duplicates.Add(path);
                }
                else
                {
                    uniqueSprites[uniqueKey] = path;
                }
            }
        }

        // Eliminar los duplicados encontrados
        foreach (string duplicatePath in duplicates)
        {
            bool success = AssetDatabase.DeleteAsset(duplicatePath);
            if (success)
            {
                Debug.Log($"Eliminado sprite duplicado: {duplicatePath}");
            }
            else
            {
                Debug.LogWarning($"No se pudo eliminar el sprite: {duplicatePath}");
            }
        }

        AssetDatabase.Refresh();
        Debug.Log($"Proceso completado. Se eliminaron {duplicates.Count} sprites duplicados.");
    }
}
