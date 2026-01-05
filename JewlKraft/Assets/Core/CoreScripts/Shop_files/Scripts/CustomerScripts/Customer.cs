using System;
using Core.CoreScripts.Inventory_files.Scripts.Databases;
using Core.Shop_files.Scripts.CustomerScripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Core.CoreScripts.Shop_files.Scripts.CustomerScripts
{
    [Serializable]
    [RequireComponent(typeof(Image))]
    public class Customer : MonoBehaviour, IPointerClickHandler
    {
        public static event Action<CraftingRecipe> OnRecipeSelected;

        [SerializeField] private CraftingRecipe recipe;
        private Image _image;

        private Image Image
        {
            get
            {
                if (_image == null)
                {
                    _image = GetComponent<Image>();
                }
                return _image;
            }
        }

        private int _daysRemaining;

        public void Initialize(Sprite image, CraftingRecipe craftingRecipe, int daysRemaining)
        {
            Image.sprite = image;
            this.recipe = craftingRecipe;
            _daysRemaining = daysRemaining;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log($"OnPointerClick: {recipe.name}");
            OnRecipeSelected?.Invoke(recipe);
        }

        public bool DayPassTimeOut()
        {
            _daysRemaining--;
            return _daysRemaining > 0;
        }
        
        
        #region Save/Load

        public CustomerData ToSaveData()
        {
            return new CustomerData(image: Image.sprite, recipe: recipe, _daysRemaining);
        }

        public void FromSaveData(CustomerData data)
        {
            Image.sprite = data.image;
            recipe = data.Recipe;
            _daysRemaining = data.daysRemaining;
        }

        #endregion
    }

    [Serializable]
    public class CustomerData
    {
        public CraftingRecipe Recipe
        {
            get
            {
                if (_recipe == null)
                {
                    _recipe = RecipeDatabase.Instance.Get(ItemDatabase.Instance.Get(rewardItemLookup));
                    if (_recipe == null) Debug.LogWarning($"No Recipe found for {_recipe.name} in database!");
                }
                return _recipe;
            }
        }

        private CraftingRecipe _recipe;
        public Sprite image;
        public ItemLookup rewardItemLookup;
        public int daysRemaining;

        public CustomerData(Sprite image, CraftingRecipe recipe, int daysRemaining)
        {
            this.image = image;
            this.rewardItemLookup = new ItemLookup(recipe.rewardItem);
            this.daysRemaining = daysRemaining;
        }
    }
}