using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.Inventory_files.Scripts.CustomerScripts
{
    [Serializable]
    [RequireComponent(typeof(Image))]
    public class Customer : MonoBehaviour, IPointerClickHandler
    {
        public static event Action<CraftingRecipe> OnRecipeSelected; 
        
        private CraftingRecipe _recipe;

        public void Initialize(Sprite image, CraftingRecipe recipe)
        {
            GetComponent<Image>().sprite = image;
            _recipe = recipe;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnRecipeSelected?.Invoke(_recipe);
        }
    }
}
