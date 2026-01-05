using System.Collections.Generic;
using Core.Inventory_files.Scripts.ItemScripts;
using Core.Shop_files.Scripts.CustomerScripts;
using UnityEngine;
using static Core.Inventory_files.Scripts.ItemScripts.Helpers;

namespace Core.CoreScripts.Inventory_files.Scripts.Databases
{
    [CreateAssetMenu(menuName = "Game/RecipeDatabase", fileName = "RecipeDatabase")]
    public class RecipeDatabase : ScriptableObject
    {
        private static RecipeDatabase _instance;

        public static RecipeDatabase Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<RecipeDatabase>("RecipeDatabase");
                    if (_instance == null)
                    {
                        Debug.LogError("RecipeDatabase not found in Resources");
                    }
                    _instance.BuildLookup();
                }

                return _instance;
            }
        }
        
        [SerializeField] private List<CraftingRecipe> ProcessedItems;
        [SerializeField] private List<CraftingRecipe> CompletedItems;
        
        private Dictionary<ItemData, CraftingRecipe> _lookup;

        private void BuildLookup()
        {
            _lookup = new();
            
            AddLookupsOfType(ProcessedItems, CraftStage.Processed);
            AddLookupsOfType(CompletedItems, CraftStage.Ready) ;
        }

        private void AddLookupsOfType(List<CraftingRecipe> items, CraftStage craftStage)
        {
            foreach (CraftingRecipe recipe in items)
            {
                if (recipe.rewardItem == null)
                {
                    Debug.LogWarning($"Item for {recipe.name} is null");
                    continue;
                }
                
                ItemData item = recipe.rewardItem;
                
                if (item.CraftStage != craftStage)
                {
                    Debug.LogWarning($"{recipe.name} " +
                                     $"has winitem {item.name} with craftStage {item.CraftStage.ToString()} " +
                                     $"instead of {craftStage.ToString()}");
                }
                
                if (!_lookup.TryAdd(item, recipe)) Debug.LogError($"Duplicate recipe for {item.name}");
            }
        }


        public CraftingRecipe Get(ItemData itemData)
        {
            return _lookup[itemData];
        }
    }
}