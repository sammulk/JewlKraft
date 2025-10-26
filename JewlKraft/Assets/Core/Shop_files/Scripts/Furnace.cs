using UnityEngine;

public class Furnace : Workstation
{
    public override void Interact()
    {
        Debug.Log("Furnace activated! Smelting items...");
        base.Interact();
    }
}
