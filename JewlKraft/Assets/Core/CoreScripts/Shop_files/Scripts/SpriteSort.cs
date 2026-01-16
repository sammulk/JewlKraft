using UnityEngine;

public class SpriteSort : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField] Transform sortPoint;
    [SerializeField] int offset = 0;
    [SerializeField] int sortingOrderBase = 100;
    //[SerializeField] bool RunOnlyOnce = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (sortPoint == null)
            sortPoint = transform;
    }

    void LateUpdate()
    {
        float yPos = sortPoint.position.y;
        spriteRenderer.sortingOrder = sortingOrderBase - Mathf.RoundToInt(yPos * 100) - offset;
    }
}
