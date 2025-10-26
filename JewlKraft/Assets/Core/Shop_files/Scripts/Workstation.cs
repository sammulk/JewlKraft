using Unity.VisualScripting;
using UnityEngine;

public class Workstation : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color defaultColor;
    [SerializeField] private Color highlightColor = new Color(
        239f / 255f,
        232f / 255f,
        243f / 255f,
        1f);

    [SerializeField] protected CanvasGroup workstationUI;

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
        ShowUI();
    }
    protected void ShowUI()
    {
        if (workstationUI != null)
        {
            workstationUI.alpha = 1f;
            workstationUI.interactable = true;
            workstationUI.blocksRaycasts = true;
        }
    }

    public void CloseUI()
    {
        if (workstationUI != null)
        {
            workstationUI.alpha = 0f;
            workstationUI.interactable = false;
            workstationUI.blocksRaycasts = false;
        }
    }

    protected virtual void Update()
    {
        if (workstationUI != null && workstationUI.alpha > 0f && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseUI();
        }
    }
}
