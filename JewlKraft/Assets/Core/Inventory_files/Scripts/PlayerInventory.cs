using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Core.Inventory_files.Scripts
{
    [Serializable]
    [CreateAssetMenu(fileName = "PlayerInventory", menuName = "Custom/Scriptable Objects/Player Inventory")]
    public class PlayerInventory : ScriptableObject
    {
        public List<StoredItem> contents;

        public int sizeX = 5;

        public int sizeY = 5;

        public void Save(List<StoredItem> playerGridContents)
        {
            contents = playerGridContents;
            string path = Path.Combine( Application.persistentDataPath, "inventory.json");
            File.WriteAllText(path, JsonUtility.ToJson(this));
        }

        public void Load()
        {
            string path = Path.Combine( Application.persistentDataPath, "inventory.json");
            if (!File.Exists(path)) return;
        
            JsonUtility.FromJsonOverwrite(File.ReadAllText(path), this);
            
            foreach (var item in contents)
            {
                item.gemData = Resources.Load<GemData>($"{item.gemID.ToString()}Asset"); // custom lookup
            }
        }
    }
}
