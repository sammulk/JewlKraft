using UnityEngine;

namespace Core.Inventory_files.Scripts
{
    [RequireComponent(typeof(RectTransform))]
    public class ItemGrid : MonoBehaviour
    {
        public const float TileSizeWidth = 32;
        public const float TileSizeHeight = 32;

        [SerializeField] private int gridWidth;
        [SerializeField] private int gridHeight;
        
        private RectTransform _rectTransform;
        private InventoryItem[,] _itemSlot;
        private Canvas _rootCanvas;

        public bool PlaceItem(InventoryItem item, int posX, int posY, ref InventoryItem overlapItem)
        {
            int dataWidth = item.Width;
            int dataHeight = item.Height;
            
            if (!BoundaryCheck(posX, posY, dataWidth, dataHeight)) return false;
            if (!OverlapCheck(posX, posY, dataWidth, dataHeight, ref overlapItem))
            {
                overlapItem = null;
                return false;
            }

            if (overlapItem != null)
            {
                ClearFromGrid(overlapItem);
            }
            
            PlaceItem(item, posX, posY);
            return true;
        }

        public void PlaceItem(InventoryItem item, int posX, int posY)
        {
            RectTransform itemRect = item.GetComponent<RectTransform>();
            itemRect.SetParent(_rectTransform);
            int dataWidth = item.Width;
            int dataHeight = item.Height;

            for (int i = 0; i < dataWidth; i++)
            {
                for (int j = 0; j < dataHeight; j++)
                {
                    _itemSlot[posX + i, posY + j] = item;
                }
            }

            var position = CalcGridPosition(item, posX, posY);

            item.gridPosX = posX;
            item.gridPosY = posY;
            itemRect.localPosition = position;
        }

        public Vector2 CalcGridPosition(InventoryItem item, int posX, int posY)
        {
            Vector2 position = new Vector2
            {
                x = posX * TileSizeWidth + TileSizeWidth * item.Width / 2,
                y = posY * TileSizeHeight + TileSizeHeight * item.Height / 2
            };
            return position;
        }

        public InventoryItem PickUpItem(int posX, int posY)
        {
            InventoryItem pickedItem = _itemSlot[posX, posY];
            if (pickedItem == null) return null;
            
            ClearFromGrid(pickedItem);
            return pickedItem;
        }

        public Vector2Int? FindSpaceFor(InventoryItem insertItem)
        {
            int width = gridWidth - insertItem.Width + 1;
            int height = gridHeight - insertItem.Height + 1;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (CheckAvailability(x, y, insertItem.Width, insertItem.Height))
                    {
                        return new Vector2Int(x, y);
                    }
                }
            }
            return null;
        }

        public bool BoundaryCheck(int posX, int posY, int width, int height)
        {
            if (!PositionCheck(posX, posY)) return false;
            posX += width -1;
            posY += height -1;
            return PositionCheck(posX, posY);
        }

        private void ClearFromGrid(InventoryItem item)
        {
            for (int i = 0; i < item.Width; i++)
            {
                for (int j = 0; j < item.Height; j++)
                {
                    _itemSlot[item.gridPosX + i, item.gridPosY + j] = null;
                }
            }
        }

        private bool OverlapCheck(int posX, int posY, int width, int height, ref InventoryItem overlapItem)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    InventoryItem item = _itemSlot[posX + i, posY + j];
                    if (item == null) continue;
                    
                    if (overlapItem == null) overlapItem = item;
                    else if (overlapItem != item) return false;
                }
            }
            return true;
        }
        
        private bool CheckAvailability(int posX, int posY, int width, int height)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (_itemSlot[posX + i, posY + j] != null)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool PositionCheck(int posX, int posY)
        {
            if (posX < 0 || posY < 0) return false;
            if (posX >= gridWidth || posY >= gridHeight) return false;
            
            return true;
        }

        public Vector2Int GetGridPosition(Vector2 mousePosition)
        {
            Vector2 localPoint = GetLocalMousePosition(mousePosition);
            
            int x = Mathf.FloorToInt(localPoint.x / TileSizeWidth * _rootCanvas.scaleFactor);
            int y = Mathf.FloorToInt(localPoint.y / TileSizeHeight * _rootCanvas.scaleFactor);

            return new Vector2Int(x, y);
        }

        private void Start()
        {
            _rootCanvas = GetComponentInParent<Canvas>().rootCanvas;
            _rectTransform = GetComponent<RectTransform>();
            InitializeSize();
        }

        private void InitializeSize()
        {
            _itemSlot = new InventoryItem[gridWidth, gridHeight];
            Vector2 size = new Vector2(gridWidth * TileSizeWidth, gridHeight * TileSizeHeight);
            _rectTransform.sizeDelta = size;
        }

        private Vector2 GetLocalMousePosition(Vector2 mousePosition)
        {
            Vector2 localMousePos = new Vector2();
            if (
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _rectTransform,
                    mousePosition,
                    //eventData.pressEventCamera,
                    null,
                    out Vector2 localPoint
                ))
            {
                localMousePos = localPoint;
            }
            
            return localMousePos;
        }

        public InventoryItem GetItem(int x, int y)
        {
            return _itemSlot[x, y];
        }
    }
}