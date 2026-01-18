using System;
using Core.Shop_files.Scripts.CustomerScripts;
using UnityEngine;

public class ChangeNotification : MonoBehaviour
{
    [SerializeField] private GameObject notificationMarker;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {
        CustomerInteract.OnRecipeSelected += DisplayMarker;
        Furnace.OnFurnaceSelected += HideMarker;
    }

    private void HideMarker()
    {
        notificationMarker.SetActive(false);
    }

    private void DisplayMarker(CraftingRecipe obj)
    {
        notificationMarker.SetActive(true);
    }
}
