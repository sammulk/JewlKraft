using UnityEngine;

public class SpriteSort : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] int offset = 0;
    [SerializeField] int sortingOrderBase = 100;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        spriteRenderer.sortingOrder = (int) (sortingOrderBase - transform.position.y - offset);
    }
}
