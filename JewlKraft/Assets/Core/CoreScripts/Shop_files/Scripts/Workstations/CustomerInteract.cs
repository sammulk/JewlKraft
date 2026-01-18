using System;
using System.Collections;
using Core.CoreScripts.Shop_files.Scripts.CustomerScripts;
using Core.Shop_files.Scripts.CustomerScripts;
using UnityEngine;
using UnityEngine.Serialization;

public class CustomerInteract : Workstation
{
    public static event Action<CraftingRecipe> OnRecipeSelected;
    
    [SerializeField] private GameObject speechBubble;
    [SerializeField] private SpriteRenderer itemRenderer;
    private CraftingRecipe recipe;

    public void SetRecipe(CraftingRecipe recipe)
    {
        this.recipe = recipe;
        itemRenderer.sprite = recipe.rewardItem.sprite;
        speechBubble.SetActive(false);
    }
    
    public override void Interact()
    {
        OnRecipeSelected?.Invoke(recipe);
        StartCoroutine(DisplayDesires());
    }

    private IEnumerator DisplayDesires()
    {
        speechBubble.SetActive(true);
        yield return new WaitForSeconds(5f);
        speechBubble.SetActive(false);
    }
}
