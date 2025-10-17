using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Core.Inventory_files.Scripts
{
    public class InventoryItem : MonoBehaviour
    {
        [SerializeField]
        public GemData gemData;

        public int gridPosX;
        public int gridPosY;
        
        public bool rotated = false;

        public int Height => !rotated ? gemData.height : gemData.width;
        public int Width => !rotated ? gemData.width : gemData.height;

        public void Init(GemData item)
        {
            gemData = item;

            GetComponent<Image>().sprite = gemData.gemSprite;

            Vector2 size = new Vector2
            {
                x = item.width * ItemGrid.TileSizeWidth,
                y = item.height * ItemGrid.TileSizeHeight
            };
        
            GetComponent<RectTransform>().sizeDelta = size;
        }

        public void Rotate()
        {
            rotated = !rotated;
            
            RectTransform rectTransform = GetComponent<RectTransform>();
            rectTransform.rotation = Quaternion.Euler(0f, 0f, rotated? 90f : 0f);
        }


        public void LoadFromStorage(StoredItem item)
        {
            Init(item.gemData);
            if (item.rotated != rotated) Rotate();
        }
        
        public StoredItem StoreItem()
        {
            return new StoredItem(gemData, gridPosX,gridPosY,rotated);
        }
    }
}