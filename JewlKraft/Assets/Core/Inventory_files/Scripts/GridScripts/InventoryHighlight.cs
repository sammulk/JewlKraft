using Core.Inventory_files.Scripts.GridScripts;
using UnityEngine;
using static Core.Inventory_files.Scripts.GridScripts.GridEssentials;

namespace Core.Inventory_files.Scripts
{
    public class InventoryHighlight : MonoBehaviour
    {
        [SerializeField] RectTransform highlightRect;

        public void Show(bool b)
        {
            highlightRect.gameObject.SetActive(b);
        }

        public void SetSize(InventoryItem item)
        {
            Vector2 size = new Vector2();
            size.x = item.Width * ItemGrid.TileSizeWidth;
            size.y = item.Height * ItemGrid.TileSizeHeight;
            highlightRect.sizeDelta = size;
        }

        public void SetPosition(ItemGrid grid, InventoryItem item)
        {
            SetParent(grid);
            
            Vector2 pos = CalcGridPosition(item, item.gridPosX, item.gridPosY);
            highlightRect.localPosition = pos;
        }

        public void SetParent(ItemGrid grid)
        {
            if (grid == null) return;
            highlightRect.SetParent(grid.GetComponent<RectTransform>());
        }

        public void SetPosition(ItemGrid grid, InventoryItem item, int posX, int posY)
        {
            Vector2 pos = CalcGridPosition(item, posX, posY);
            highlightRect.localPosition = pos;
        }
    }
}
