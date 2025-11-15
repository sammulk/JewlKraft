using System;
using UnityEngine;
using UnityEngine.Serialization;
using static Core.Inventory_files.Scripts.ItemScripts.Helpers;

namespace Core.Inventory_files.Scripts.ItemScripts
{
    /**
     * positional storable data of an object
     */
    [Serializable]
    public class StoredItem : IEquatable<StoredItem>
    {
        [NonSerialized] private ItemData _itemData;

        public MaterialType materialID;
        public CraftStage craftStageID;
        
        public int gridPosX;
        public int gridPosY;

        public bool rotated = false;

        public ItemData ItemData
        {
            get
            {
                if (_itemData != null) return _itemData;
                
                _itemData = ItemDatabase.Instance.Get(materialID, craftStageID);
                if (_itemData == null) Debug.LogError($"{materialID}, {craftStageID} not found in ItemDatabase");
                return _itemData;
            }
        }
        public StoredItem(ItemData itemData, int gridPosX, int gridPosY, bool rotated)
        {
            this._itemData = itemData;
            this.materialID = itemData.MaterialType;
            this.gridPosX = gridPosX;
            this.gridPosY = gridPosY;
            this.rotated = rotated;
        }

        public bool Equals(StoredItem other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return materialID == other.materialID && gridPosX == other.gridPosX && gridPosY == other.gridPosY &&
                   rotated == other.rotated;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((StoredItem)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(materialID, gridPosX, gridPosY, rotated);
        }
    }
}