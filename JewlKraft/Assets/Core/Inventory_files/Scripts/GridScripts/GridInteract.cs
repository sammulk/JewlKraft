using Core.Inventory_files.Scripts.GridScripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Core.Inventory_files.Scripts
{
    [RequireComponent(typeof(ItemGrid))]
    public class GridInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private InventoryController inventoryController;
        private ItemGrid _itemGrid;
    
        void Start()
        {
            _itemGrid = GetComponent<ItemGrid>();
        }
    
        public void OnPointerEnter(PointerEventData eventData)
        {
            inventoryController.SelectedGrid = _itemGrid;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            inventoryController.SelectedGrid = null;
        }
    }
}
