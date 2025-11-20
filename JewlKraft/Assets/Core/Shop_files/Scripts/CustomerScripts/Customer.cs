using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.Shop_files.Scripts.CustomerScripts
{
    [Serializable]
    [RequireComponent(typeof(Image))]
    public class Customer : MonoBehaviour, IPointerClickHandler
    {
        public static event Action<CraftingRecipe> OnRecipeSelected;

        private CraftingRecipe _recipe;
        private int _daysRemaining;
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

        public void Initialize(Sprite image, CraftingRecipe recipe, int daysRemaining)
        {
            Image.sprite = image;
            _recipe = recipe;
            _daysRemaining = daysRemaining;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnRecipeSelected?.Invoke(_recipe);
        }

        public bool DayPassTimeOut()
        {
            _daysRemaining--;
            return _daysRemaining > 0;
        }
        
        
        #region Save/Load

        public CustomerData ToSaveData()
        {
            return new CustomerData(image: Image.sprite, recipe: _recipe, _daysRemaining);
        }

        public void FromSaveData(CustomerData data)
        {
            Image.sprite = data.image;
            _recipe = data.recipe;
            _daysRemaining = data.daysRemaining;
        }

        #endregion
    }

    [Serializable]
    public class CustomerData
    {
        public Sprite image;
        public CraftingRecipe recipe;
        public int daysRemaining;

        public CustomerData(Sprite image, CraftingRecipe recipe, int daysRemaining)
        {
            this.image = image;
            this.recipe = recipe;
            this.daysRemaining = daysRemaining;
        }
    }
}