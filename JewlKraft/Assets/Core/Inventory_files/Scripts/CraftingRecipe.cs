using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Inventory_files.Scripts
{
    [CreateAssetMenu(fileName = "CraftingRecipe", menuName = "Custom/Scriptable Objects/CraftingRecipe")]
    [Serializable]
    public class CraftingRecipe : ScriptableObject
    {
        public List<GridData> itemsPerGrid = new();
        public GemData rewardItem;
    }

    [Serializable]
    public class GridData
    {
        public List<StoredItem> items = new();
        //coordinates of root (bottom left)
        public int gridPosX;
        public int gridPosY;
        
        public int width => items.Max(i => i.gridPosX) + 1;
        public int Height => items.Max(i => i.gridPosY) + 1;
    }
}
