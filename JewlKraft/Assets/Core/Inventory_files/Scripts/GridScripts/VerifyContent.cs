using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Inventory_files.Scripts.GridScripts
{
    [RequireComponent(typeof(ItemGrid))]
    public class VerifyContent : MonoBehaviour
    {
        public event Action OnCorrectContents;
        
        [SerializeField]
        private List<StoredItem> items = new();
        
        private ItemGrid _itemGrid;
        
        private void Awake()
        {
            _itemGrid = GetComponent<ItemGrid>();
        }
        
        private void Start()
        {
            //LoadInventory(_itemGrid, items);
            foreach (InventoryItem item in _itemGrid.Contents.ToList())
            {
                var color = item.GetComponent<Image>().color;
                color.a = 130f / 255f;
                item.GetComponent<Image>().color = color;
                _itemGrid.ClearFromGrid(item);
            }
            _itemGrid.OnItemPlaced += CheckGridContents;
        }

        private void OnDestroy()
        {
            _itemGrid.OnItemPlaced -= CheckGridContents;
        }

        public void Initialize(List<StoredItem> assignedItems)
        {
            items = assignedItems;
        }

        public bool CompareContents()
        {
            return _itemGrid.Contents.Select(i => i.StoreItem()).ToHashSet().SetEquals(items);
        }

        private void CheckGridContents()
        {
            if (CompareContents()) OnCorrectContents?.Invoke();
        }
    }
}
