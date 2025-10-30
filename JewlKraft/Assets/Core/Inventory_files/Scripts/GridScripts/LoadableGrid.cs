using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Core.Inventory_files.Scripts.GridScripts.GridEssentials;

namespace Core.Inventory_files.Scripts.GridScripts
{
    [RequireComponent(typeof(ItemGrid))]
    public class LoadableGrid : MonoBehaviour
    {
        private ItemGrid _playerGrid;
        private PlayerInventory _playerItems;

        private void Awake()
        {
            _playerGrid = GetComponent<ItemGrid>();
        }

        private void Start()
        {
            LoadPlayerInventory();
        }

        private void OnDestroy()
        {
            SavePlayerInventory();
        }

        private void SavePlayerInventory()
        {
            if (_playerItems == null) _playerItems = Resources.Load("PlayerInventory") as PlayerInventory;

            System.Diagnostics.Debug.Assert(_playerItems != null, nameof(_playerItems) + " != null");

            List<StoredItem> storedItems = _playerGrid.Contents.Select(item => item.StoreItem()).ToList();
            _playerItems.Save(storedItems);
        }

        private void LoadPlayerInventory()
        {
            _playerItems = Resources.Load("PlayerInventory") as PlayerInventory;

            System.Diagnostics.Debug.Assert(_playerItems != null, nameof(_playerItems) + " != null");
            _playerItems.Load();

            _playerGrid.InitializeSize(_playerItems.sizeX, _playerItems.sizeY);
            LoadInventory(_playerGrid, _playerItems.contents);
        }
    }
}