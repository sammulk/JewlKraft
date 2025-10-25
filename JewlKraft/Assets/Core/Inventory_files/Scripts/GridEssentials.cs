using System.Collections.Generic;
using Core.Dungeon_files.Scripts;
using UnityEngine;
using static Core.Inventory_files.Scripts.ItemGrid;

namespace Core.Inventory_files.Scripts
{
    public static class GridEssentials
    {
        public static void LoadInventory(ItemGrid targetInventory, List<StoredItem> inventoryItems)
        {
            if (targetInventory.Contents.Count != 0) Debug.LogError("Loading a loaded inventory");
            if (inventoryItems.Count == 0) Debug.Log("Loading an empty inventory");

            foreach (StoredItem item in inventoryItems)
            {
                if (item.gemData == null)
                {
                    item.gemData = Resources.Load<GemData>($"{item.gemID.ToString()}Asset"); 
                    if (item.gemData == null) Debug.LogError($"{item.gemID.ToString()}Asset not found");
                }
                
                InventoryItem newItem = InventoryFactory.Instance.Create(item.gemData, item.rotated);
                InsertItemAt(targetInventory, newItem, item.gridPosX, item.gridPosY);
            }
        }
        
        public static void InsertItem(ItemGrid targetGrid, InventoryItem insertItem)
        {
            Vector2Int? gridPos = targetGrid.FindSpaceFor(insertItem);

            if (!gridPos.HasValue) return;

            targetGrid.PlaceItem(insertItem, gridPos.Value.x, gridPos.Value.y);
        }

        /**
         * Doesn't check for overlapping!!
         */
        public static void InsertItemAt(ItemGrid targetGrid, InventoryItem targetItem, int gridPosX, int gridPosY)
        {
            targetGrid.PlaceItem(targetItem, gridPosX, gridPosY);
        }
        
        public static Vector2 CalcGridPosition(InventoryItem item, int posX, int posY)
        {
            Vector2 position = new Vector2
            {
                x = posX * TileSizeWidth + TileSizeWidth * item.Width / 2,
                y = posY * TileSizeHeight + TileSizeHeight * item.Height / 2
            };
            return position;
        }
    }
}
