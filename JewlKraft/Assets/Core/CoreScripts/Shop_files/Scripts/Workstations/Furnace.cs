using System;
using UnityEngine;

public class Furnace : Workstation
{
    public static event Action OnFurnaceSelected;
    
    public override void Interact()
    {
        Debug.Log("Furnace activated! Smelting items...");
        base.Interact();
        GetComponent<TutorialBubble>()?.HideBubble();
        OnFurnaceSelected?.Invoke();
    }
}
