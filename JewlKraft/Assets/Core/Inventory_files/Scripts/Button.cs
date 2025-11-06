using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class InventoryButton : MonoBehaviour
{
    [SerializeField] private GameObject UIButton;
    private Boolean isOpen = false;

    private void Awake()
    {
        UIButton.GetComponent<Button>().onClick.AddListener(() => { Inventory(); });
    }

    void Start()
    {
        CanvasGroup canvas = GetComponent<CanvasGroup>();
        canvas.alpha = 0f;
        canvas.interactable = false;
        canvas.blocksRaycasts = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Inventory();
        }
    }

    void Inventory()
    {
        CanvasGroup canvas = GetComponent<CanvasGroup>();
        if (isOpen)
        {
            isOpen = false;
            canvas.alpha = 0f;
            canvas.interactable = false;
            canvas.blocksRaycasts = false;
            return;
        }
        else
        {
            isOpen = true;
            canvas.alpha = 1f;
            canvas.interactable = true;
            canvas.blocksRaycasts = true;
        }
    }
}


