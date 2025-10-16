using UnityEngine;

public class Workstation : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color defaultColor;
    [SerializeField] private Color highlightColor = Color.white;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
    }

    public void SetHighlight(bool active)
    {
        spriteRenderer.color = active ? highlightColor : defaultColor;
    }

    public virtual void Interact()
    {
        Debug.Log("Interacted with generic workstation: " + name);
    }
}
