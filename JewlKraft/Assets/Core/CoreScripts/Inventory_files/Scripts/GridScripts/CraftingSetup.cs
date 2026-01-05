using Core.CoreScripts.Inventory_files.Scripts.GridScripts;
using Core.CoreScripts.Shop_files.Scripts.CustomerScripts;
using Core.Shop_files.Scripts.CustomerScripts;
using Unity.VisualScripting;
using UnityEngine;

namespace Core.Inventory_files.Scripts.GridScripts
{
    [RequireComponent(typeof(CraftingTable))]
    public class CraftingSetup : MonoBehaviour
    {
        private const float Padding = 10;

        [SerializeField] private CraftingRecipe recipe;

        private CraftingTable _craftingTable;

        void Awake()
        {
            _craftingTable = GetComponent<CraftingTable>();
        }

        private void OnEnable()
        {
            Customer.OnRecipeSelected += HandleRecipeChange;
        }

        private void OnDisable()
        {
            Customer.OnRecipeSelected -= HandleRecipeChange;
        }

        private void HandleRecipeChange(CraftingRecipe newRecipe)
        {
            if (newRecipe == recipe)
            {
                if (recipe == null) Debug.LogError("Recipe is null");
                
                Debug.Log("Recipe kept same");
                return;
            }

            _craftingTable.Cleanup();
            SetupCraftingTable(newRecipe);
        }

        private void SetupCraftingTable(CraftingRecipe newRecipe)
        {
            recipe = newRecipe;

            Debug.Log($"got recipe with {newRecipe.rewardItem.name} " +
                      $"and length {recipe.itemsPerGrid.Count}");
            foreach (GridData data in recipe.itemsPerGrid)
            {
                ItemGrid newGrid = GridFactory.Instance.Create(data.Width, data.Height, transform, data.items);
                newGrid.AddComponent<VerifyContent>().Initialize(data.items);
                newGrid.transform.localPosition = new Vector3(
                    ItemGrid.TileSizeWidth * data.gridPosX + Padding,
                    ItemGrid.TileSizeHeight * data.gridPosY + Padding, 0);
            }

            _craftingTable.SetupReady(recipe.rewardItem);

            RectTransform rect = GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(ItemGrid.TileSizeWidth * 3 + 2 * Padding,
                ItemGrid.TileSizeHeight * 3 + 2 * Padding);
        }
    }
}