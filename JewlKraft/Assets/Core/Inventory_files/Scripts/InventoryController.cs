using System.Collections;
using System.Collections.Generic;
using Core.Dungeon_files.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;
using static Core.Inventory_files.Scripts.GridEssentials;



namespace Core.Inventory_files.Scripts
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private ItemGrid playerGrid;

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

        private void OnEnable()
        {
            GemShard.OnGemShardPickedUp += AddShardFromFloor;
            CraftingTable.OnCraftingComplete += ReceiveCraftedItem;
        }

        private void OnDisable()
        {
            GemShard.OnGemShardPickedUp -= AddShardFromFloor;
            CraftingTable.OnCraftingComplete -= ReceiveCraftedItem;
        }

        private void Update()
        {
            Vector3 mousePosition = Input.mousePosition;
            DragItem(mousePosition);

            if (Input.GetKeyDown(KeyCode.Q)) CreateRandomItem();

            //if (Input.GetKeyDown(KeyCode.W)) InsertRandomItem();

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
        
        private void InsertRandomItem()
        {
            if (SelectedGrid == null) return;

            CreateRandomItem();
            InventoryItem insertItem = _selectedItem;
            _selectedItem = null;
            _selectedTransform = null;
            InsertItem(insertItem);
        }

        private void AddShardFromFloor(GemShard shard)
        {
            if (playerGrid == null) return;
            bool needsRotation = false;
            Vector2Int? spaceFor = playerGrid.FindSpaceFor(shard.gemData.width, shard.gemData.height);
            
            if (!spaceFor.HasValue)
            {
                //what if you rotate
                spaceFor = playerGrid.FindSpaceFor(shard.gemData.height, shard.gemData.width);
                if (!spaceFor.HasValue) return;
                needsRotation = true;
            }
            
            Vector2Int targetGridPosition = spaceFor.Value;
            InventoryItem newItem = InventoryFactory.Instance.Create(shard.gemData, needsRotation);

            InsertItemAt(playerGrid, newItem, targetGridPosition.x, targetGridPosition.y);
            Destroy(shard.gameObject);
        }

        private void InsertItem(InventoryItem insertItem)
        {
            GridEssentials.InsertItem(_selectedGrid, insertItem);
        }
        
        private void PlaceItemAt(Vector2Int gridPosition)
        {
            bool completed = _selectedGrid.PlaceItem(_selectedItem, gridPosition.x, gridPosition.y, ref _overlapItem);

            if (!completed) return;
            
            _selectedItem = null;
            _selectedTransform = null;
            
            if (_overlapItem == null) return;
            
            _selectedItem = _overlapItem;
            _overlapItem = null;
            _selectedTransform = _selectedItem.GetComponent<RectTransform>();
            _selectedTransform.SetAsLastSibling();
        }

        private void PickUpItemAt(Vector2Int gridPosition)
        {
            _selectedItem = _selectedGrid.PickUpItem(gridPosition.x, gridPosition.y);
            if (_selectedItem == null) return;

            _selectedTransform = _selectedItem.GetComponent<RectTransform>();
            _selectedTransform.SetParent(rootTransform);
            _selectedTransform.SetAsLastSibling();
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

        private void DragItem(Vector3 mousePosition)
        {
            if (_selectedTransform != null) {_selectedTransform.position = mousePosition;}
        }

        private void CreateRandomItem()
        {
            if (_selectedItem != null) return;

            int selectedIndex = Random.Range(0, items.Count);
            InventoryItem newItem = InventoryFactory.Instance.Create(items[selectedIndex]);
            
            RectTransform rectTransform = newItem.GetComponent<RectTransform>();
            rectTransform.SetParent(rootTransform);
            rectTransform.SetAsLastSibling();

            _selectedItem = newItem;
            _selectedTransform = rectTransform;
        }

        private Vector2Int GetRootCoordinate(Vector3 mousePosition)
        {
            if (_selectedItem != null)
            {
                mousePosition.x -= (_selectedItem.Width - 1) * ItemGrid.TileSizeWidth / 2;
                mousePosition.y -= (_selectedItem.Height - 1) * ItemGrid.TileSizeHeight / 2;
            }

            Vector2Int gridPosition = _selectedGrid.GetGridPosition(mousePosition);
            return gridPosition;
        }

        private void ReceiveCraftedItem(InventoryItem item)
        {
            StartCoroutine(NewItemCoroutine(item));
        }

        private IEnumerator NewItemCoroutine(InventoryItem item)
        {
            yield return null;
            SelectedGrid = null;
            
            RectTransform rectTransform = item.GetComponent<RectTransform>();
            
            _selectedItem = item;
            _selectedTransform = rectTransform;
            _selectedTransform.SetParent(rootTransform);
            _selectedTransform.SetAsLastSibling();
            _highlightTarget = item;
            
            DragItem(Input.mousePosition);
        }
    }
}