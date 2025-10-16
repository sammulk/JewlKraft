using System.Collections.Generic;
using UnityEngine;


namespace Core.Inventory_files.Scripts
{
    public class InventoryController : MonoBehaviour
    {
        private ItemGrid _selectedGrid;

        public ItemGrid SelectedGrid
        {
            get => _selectedGrid;
            set
            {
                _selectedGrid = value;
                _inventoryHighlight.SetParent(value);
            }
        }

        [SerializeField] List<GemData> items;
        [SerializeField] InventoryItem itemPrefab;
        [SerializeField] private RectTransform rootTransform;
        
        private InventoryHighlight _inventoryHighlight;
        private InventoryItem _highlightTarget;
        private Vector2Int _lastHighlightPos;


        private InventoryItem _selectedItem;
        private InventoryItem _overlapItem;
        private RectTransform _selectedTransform;

        private void Start()
        {
            _inventoryHighlight = GetComponent<InventoryHighlight>();
        }

        private void Update()
        {
            Vector3 mousePosition = Input.mousePosition;
            DragItem(mousePosition);

            if (Input.GetKeyDown(KeyCode.Q)) CreateRandomItem();

            if (Input.GetKeyDown(KeyCode.W)) InsertRandomItem();
            
            if (Input.GetKeyDown(KeyCode.R)) RotateItem();
            
            if (_selectedGrid == null)
            {
                _inventoryHighlight.Show(false);
                return;
            }
            HandleHighlight(mousePosition);

            if (Input.GetMouseButtonDown(0)) HandleClick(mousePosition);
        }

        private void RotateItem()
        {
            if (_selectedItem == null) return;
            _selectedItem.Rotate();
        }

        private void InsertRandomItem()
        {
            if (SelectedGrid == null) return;
            
            CreateRandomItem();
            InventoryItem insertItem = _selectedItem;
            _selectedItem = null;
            _selectedTransform = null;
            InsertItem(insertItem);
        }

        private void InsertItem(InventoryItem insertItem)
        {
            Vector2Int? gridPos = _selectedGrid.FindSpaceFor(insertItem);

            if (!gridPos.HasValue) return;
            
            _selectedGrid.PlaceItem(insertItem, gridPos.Value.x, gridPos.Value.y);
        }

        private void HandleClick(Vector3 mousePosition)
        {
            Vector2Int gridPosition = GetRootCoordinate(mousePosition);

            if (_selectedItem == null)
            {
                PickUpItemAt(gridPosition);
            }
            else
            {
                PlaceItemAt(gridPosition);
            }
        }

        private void HandleHighlight(Vector2 mousePosition)
        {
            Vector2Int positionOnGrid = GetRootCoordinate(mousePosition);
            if (positionOnGrid == _lastHighlightPos) return;
            
            _lastHighlightPos = positionOnGrid;
            if (_selectedItem == null)
            {
                _highlightTarget = _selectedGrid.GetItem(positionOnGrid.x, positionOnGrid.y);
                
                if (_highlightTarget != null)
                {
                    _inventoryHighlight.Show(true);
                    _inventoryHighlight.SetSize(_highlightTarget);
                    _inventoryHighlight.SetPosition(_selectedGrid, _highlightTarget);
                }
                else
                {
                    _inventoryHighlight.Show(false);
                }
            }
            else
            {
                _inventoryHighlight.Show(_selectedGrid.BoundaryCheck(
                    positionOnGrid.x,
                    positionOnGrid.y,
                    _selectedItem.Width,
                    _selectedItem.Height));
                _inventoryHighlight.SetSize(_selectedItem);
                _inventoryHighlight.SetPosition(_selectedGrid, _selectedItem, positionOnGrid.x, positionOnGrid.y);
            }
        }

        private Vector2Int GetRootCoordinate(Vector3 mousePosition)
        {
            if (_selectedItem != null)
            {
                mousePosition.x -= (_selectedItem.Width - 1) * ItemGrid.TileSizeWidth / 2;
                mousePosition.y -= (_selectedItem.Height -1) * ItemGrid.TileSizeHeight / 2;
            }
            
            Vector2Int gridPosition = _selectedGrid.GetGridPosition(mousePosition);
            return gridPosition;
        }

        private void PlaceItemAt(Vector2Int gridPosition)
        {
            bool completed = _selectedGrid.PlaceItem(_selectedItem, gridPosition.x, gridPosition.y, ref _overlapItem);

            if (completed)
            {
                _selectedItem = null;
                _selectedTransform = null;
                if (_overlapItem != null)
                {
                    _selectedItem = _overlapItem;
                    _overlapItem = null;
                    _selectedTransform = _selectedItem.GetComponent<RectTransform>();
                    _selectedTransform.SetAsLastSibling();
                }
            }
        }

        private void PickUpItemAt(Vector2Int gridPosition)
        {
            _selectedItem = _selectedGrid.PickUpItem(gridPosition.x, gridPosition.y);
            if (_selectedItem == null) return;
            
            _selectedTransform = _selectedItem.GetComponent<RectTransform>();
            _selectedTransform.SetParent(rootTransform);
            _selectedTransform.SetAsLastSibling();
        }

        private void DragItem(Vector3 mousePosition)
        {
            if (_selectedTransform != null) _selectedTransform.position = mousePosition;
        }

        private void CreateRandomItem()
        {
            if (_selectedItem != null) return;
            
            InventoryItem newItem = Instantiate(itemPrefab);
            RectTransform rectTransform = newItem.GetComponent<RectTransform>();
            rectTransform.SetParent(rootTransform);
            rectTransform.SetAsLastSibling();

            int selectedIndex = Random.Range(0, items.Count);
            newItem.Init(items[selectedIndex]);
            
            _selectedItem = newItem;
            _selectedTransform = rectTransform;
        }
    }
}
