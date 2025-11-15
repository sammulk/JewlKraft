using System;
using System.Collections.Generic;
using System.Linq;
using Core.Inventory_files.Scripts.ItemScripts;
using UnityEngine;

namespace Core.Inventory_files.Scripts.CustomerScripts
{
    [CreateAssetMenu(fileName = "CraftingRecipe", menuName = "Custom/Scriptable Objects/CraftingRecipe")]
    [Serializable]
    public class CraftingRecipe : ScriptableObject
    {
        [SerializeField] public List<GridData> itemsPerGrid = new();
        [SerializeField] public ItemData rewardItem;
    }

    [Serializable]
    public class GridData
    {
        public List<StoredItem> items = new();
        //coordinates of root (bottom left)
        public int gridPosX;
        public int gridPosY;
        
        public int Width => items.Max(i => i.gridPosX + i.ItemData.width);
        public int Height => items.Max(i => i.gridPosY + i.ItemData.height);
    }
}
