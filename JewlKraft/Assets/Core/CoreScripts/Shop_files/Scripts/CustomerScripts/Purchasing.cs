using System;
using Core.CoreScripts.Inventory_files.Scripts.Databases;
using Core.Shop_files.Scripts.CustomerScripts;
using UnityEngine;

namespace Core.CoreScripts.Shop_files.Scripts.CustomerScripts
{
    public class Purchasing : MonoBehaviour
    {
        
        public event Action OnDesireAcquired;

        private CraftingRecipe _recipe;

        public void SetRecipe(CraftingRecipe recipe)
        {
            _recipe = recipe;
        }

        public bool OfferDesire(ItemLookup itemLookup)
        {
            bool wasCorrect = ConfirmGotDesire(itemLookup);
            
            if (wasCorrect) GoldController.Instance.AddGold(_recipe.rewardItem.goldValue);
            
            OnDesireAcquired?.Invoke();
            return wasCorrect;
        }
        
        private bool ConfirmGotDesire(ItemLookup itemLookup)
        {
            ItemLookup ownLookup = new ItemLookup(_recipe.rewardItem);
            return ownLookup.CompareToLookup(itemLookup);
        }
    }
}
