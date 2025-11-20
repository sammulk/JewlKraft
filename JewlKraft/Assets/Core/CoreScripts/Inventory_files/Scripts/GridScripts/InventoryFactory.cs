using Core.Inventory_files.Scripts.ItemScripts;
using UnityEngine;

namespace Core.CoreScripts.Inventory_files.Scripts.GridScripts
{
    public class InventoryFactory : MonoBehaviour
    {
        public static InventoryFactory Instance { get; private set; }
        
        [SerializeField] private InventoryItem itemPrefab;

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
        
        public InventoryItem Create(ItemData data, bool needsRotation = false)
        {
            InventoryItem newItem = Instantiate(itemPrefab);
            newItem.Init(data);
            if (needsRotation) newItem.Rotate();
            return newItem;
        }
    }
}