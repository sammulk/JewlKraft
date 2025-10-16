using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace Core.Inventory_files.Scripts
{
    [Serializable]
    public class PlayerInventory
    {
        public List<InventoryItem> contents;

        public int sizeX = 10;
        public int sizeY = 10;
        
        
    }
}
