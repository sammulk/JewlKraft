using System;
using System.Collections.Generic;
using System.IO;
using Core.Inventory_files.Scripts.ItemScripts;
using UnityEngine;

namespace Core.Inventory_files.Scripts
{
    [Serializable]
    [CreateAssetMenu(fileName = "PlayerInventory", menuName = "Custom/Player Inventory")]
    public class PlayerInventory : ScriptableObject, ISaveable<PlayerInventorySaveData>
    {
        public List<StoredItem> contents;

        public int sizeX = 5;

        public int sizeY = 5;
        
        public PlayerInventorySaveData ToSaveData()
        {
            return new PlayerInventorySaveData
            {
                items = new List<StoredItem>(contents)
            };
        }

        public void FromSaveData(PlayerInventorySaveData data)
        {
            contents = new List<StoredItem>(data.items);
        }
        
    }
    
    [Serializable]
    public class PlayerInventorySaveData
    {
        public List<StoredItem> items;
    }

}
