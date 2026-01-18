using System.Collections.Generic;
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

    private static readonly List<Workstation> _openWorkstations = new List<Workstation>();

    public static int LastEscapeHandledFrame { get; private set; } = -1;

    public static bool AnyOpen => _openWorkstations.Count > 0;

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

            if (!_openWorkstations.Contains(this))
                _openWorkstations.Add(this);
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

        _openWorkstations.Remove(this);
    }

    public static void CloseAllOpen()
    {
        var copy = _openWorkstations.ToArray();
        foreach (var ws in copy)
        {
            if (ws != null)
                ws.CloseUI();
        }
    }

    protected virtual void Update()
    {
        if (workstationUI != null && workstationUI.alpha > 0f && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseUI();
            LastEscapeHandledFrame = Time.frameCount;
        }
    }
}
