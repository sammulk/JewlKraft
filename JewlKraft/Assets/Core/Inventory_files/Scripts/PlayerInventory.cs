using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Inventory_files.Scripts
{
    [Serializable]
    public class StoredItem
    {
        [NonSerialized] public GemData gemData;

        public string gemID;
        public int gridPosX;
        public int gridPosY;
        
        public bool rotated = false;

        public StoredItem(GemData gemData, int gridPosX, int gridPosY, bool rotated)
        {
            this.gemData = gemData;
            this.gemID = gemData.name;
            this.gridPosX = gridPosX;
            this.gridPosY = gridPosY;
            this.rotated = rotated;
        }
    }
    
    
    [Serializable]
    [CreateAssetMenu(fileName = "PlayerInventory", menuName = "Custom/Scriptable Objects/Player Inventory")]
    public class PlayerInventory : ScriptableObject
    {
        public List<StoredItem> contents;

        public int sizeX = 10;

        public int sizeY = 10;

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
                item.gemData = Resources.Load<GemData>(item.gemID); // custom lookup
            }
        }
    }
}
