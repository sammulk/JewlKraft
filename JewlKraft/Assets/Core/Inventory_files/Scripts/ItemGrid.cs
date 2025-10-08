using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.Core_Scripts
{
    [RequireComponent(typeof(RectTransform))]
    public class ItemGrid : MonoBehaviour, IPointerMoveHandler
    {
        private const float TileSizeWidth = 32;
        private const float TileSizeHeight = 32;

        [SerializeField] private int width;
        [SerializeField] private int height;

        private RectTransform _rectTransform;
        private InventoryItem[,] _itemSlot;

        //private Vector2 _positionOnTheGrid = new Vector2();
        //private Vector2Int _tileGridPosition = new Vector2Int();


        private void Initialize(int width, int height)
        {
            _itemSlot = new InventoryItem[width, height];
            Vector2 size = new Vector2(width * TileSizeWidth, height * TileSizeHeight);
            _rectTransform.sizeDelta = size;
        }

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            Initialize(width, height);
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            if (!GetLocalMousePosition(eventData, out Vector2 localMousePos)) return;

            Vector2Int gridPos = GetGridPosition(localMousePos);
            Debug.Log($"Hovering cell: {gridPos}");
        }

        private bool GetLocalMousePosition(PointerEventData eventData, out Vector2 localMousePos)
        {
            if (
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _rectTransform,
                    eventData.position,
                    //eventData.pressEventCamera,
                    null,
                    out Vector2 localPoint
                ))
            {
                localMousePos = localPoint;
                return true;
            }

            localMousePos = Vector2.zero;
            return false;
        }

        private Vector2Int GetGridPosition(Vector2 localPoint)
        {
            Canvas rootCanvas = GetComponentInParent<Canvas>().rootCanvas;
            int x = Mathf.FloorToInt(localPoint.x / TileSizeWidth * rootCanvas.scaleFactor);
            int y = Mathf.FloorToInt(localPoint.y / TileSizeHeight * rootCanvas.scaleFactor);

            return new Vector2Int(x, y);
        }
    }
}