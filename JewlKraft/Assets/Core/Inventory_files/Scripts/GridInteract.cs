using Core.Core_Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.Scripts
{
    [RequireComponent(typeof(ItemGrid))]
    public class GridInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private ItemGrid _itemGrid;
    
        void Start()
        {
            _itemGrid = GetComponent<ItemGrid>();
        }
    
        public void OnPointerEnter(PointerEventData eventData)
        {
        
        }

        public void OnPointerExit(PointerEventData eventData)
        {
        
        }
    }
}
