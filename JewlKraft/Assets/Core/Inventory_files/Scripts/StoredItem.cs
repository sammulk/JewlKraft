using System;

namespace Core.Inventory_files.Scripts
{
    /**
     * positional storable data of an object
     */
    [Serializable]
    public class StoredItem : IEquatable<StoredItem>
    {
        [NonSerialized] public GemData gemData;

        public GemTypes gemID;
        public int gridPosX;
        public int gridPosY;

        public bool rotated = false;

        public StoredItem(GemData gemData, int gridPosX, int gridPosY, bool rotated)
        {
            this.gemData = gemData;
            this.gemID = gemData.gemType;
            this.gridPosX = gridPosX;
            this.gridPosY = gridPosY;
            this.rotated = rotated;
        }

        public bool Equals(StoredItem other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return gemID == other.gemID && gridPosX == other.gridPosX && gridPosY == other.gridPosY &&
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
            return HashCode.Combine(gemID, gridPosX, gridPosY, rotated);
        }
    }
}