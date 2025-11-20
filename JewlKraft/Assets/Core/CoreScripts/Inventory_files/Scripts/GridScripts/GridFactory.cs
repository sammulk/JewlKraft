using System.Collections.Generic;
using Core.Inventory_files.Scripts;
using Core.Inventory_files.Scripts.GridScripts;
using Core.Inventory_files.Scripts.ItemScripts;
using UnityEngine;
using UnityEngine.Serialization;

public class GridFactory : MonoBehaviour
{
    public static GridFactory Instance { get; private set; }
        
    [SerializeField] private ItemGrid gridPrefab;
    [SerializeField] private InventoryController controller;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
        
    public ItemGrid Create(int width, int height, Transform parent, List<StoredItem> items = null)
    {
        ItemGrid grid = Instantiate(gridPrefab, parent.transform);
        grid.GetComponent<GridInteract>().inventoryController = controller;
        grid.InitializeSize(width, height);
        if (items != null) GridEssentials.LoadInventory(grid, items);
        return grid;
    }
}
