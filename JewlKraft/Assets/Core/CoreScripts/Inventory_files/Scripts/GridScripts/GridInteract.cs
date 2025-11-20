using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.Inventory_files.Scripts.GridScripts
{
    [RequireComponent(typeof(ItemGrid))]
    public class GridInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        public InventoryController inventoryController;
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
