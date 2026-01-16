using System;
using Core.CoreScripts.Inventory_files.Scripts.Databases;
using Core.Shop_files.Scripts.CustomerScripts;
using UnityEngine;
using UnityEngine.U2D.Animation;

namespace Core.CoreScripts.Shop_files.Scripts.CustomerScripts
{
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
                    else
                    {
                        Debug.Log($"Recipe returned for {_recipe.name}");
                    }
                }
                return _recipe;
            }
        }
        private CraftingRecipe _recipe;
        
        public SpriteLibraryAsset SpriteLibrary
        {
            get
            {
                if (_spriteLibrary == null)
                {
                    _spriteLibrary = CustomerDatabase.Instance.GetAsset(spriteID);
                    if (_spriteLibrary == null) Debug.LogWarning($"No spriteLib found for id {spriteID} in database!");
                }
                return _spriteLibrary;
            }
        }
        private SpriteLibraryAsset _spriteLibrary;


        #region Saveable data fields
        
        [HideInInspector] public int spriteID;
        public ItemLookup rewardItemLookup;
        public int daysRemaining;
        
        #endregion

        public CustomerData(int spriteID, CraftingRecipe recipe, int daysRemaining = 3)
        {
            this.spriteID = spriteID;
            this.rewardItemLookup = new ItemLookup(recipe.rewardItem);
            this.daysRemaining = daysRemaining;
        }
    }
}