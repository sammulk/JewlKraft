using Core.CoreScripts.Inventory_files.Scripts.GridScripts;
using Core.Inventory_files.Scripts.GridScripts;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Inventory_files.Scripts.ItemScripts
{
    [RequireComponent(typeof(Image))]
    public class InventoryItem : MonoBehaviour
    {
        [SerializeField]
        public ItemData itemData;

        public int gridPosX;
        public int gridPosY;
        
        public bool rotated = false;

        public int Height => !rotated ? itemData.height : itemData.width;
        public int Width => !rotated ? itemData.width : itemData.height;

        public void Init(ItemData item)
        {
            itemData = item;

            GetComponent<Image>().sprite = itemData.sprite;

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
        
        public StoredItem StoreItem()
        {
            return new StoredItem(itemData, gridPosX,gridPosY,rotated);
        }
    }
}