using UnityEngine;

public class Sanding : Workstation
{
    public override void Interact()
    {
        Debug.Log("Sanding table activated! Sanding gems...");
        base.Interact();
    }
}
