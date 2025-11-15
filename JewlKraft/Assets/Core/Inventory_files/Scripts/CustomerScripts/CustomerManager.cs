using System.Collections.Generic;
using Core.Inventory_files.Scripts.ItemScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Inventory_files.Scripts.CustomerScripts
{
    public class CustomerManager : MonoBehaviour
    {
        [SerializeField] private Customer customerPrefab;

        [Header("Spawn variables")] 
        
        [SerializeField]
        private List<Sprite> customerImages = new();
        [SerializeField] 
        private List<CraftingRecipe> itemRecipes = new();

        void OnEnable()
        {
            CustomerController.OnCustomerNeeded += AddCustomer;
        }

        private void AddCustomer()
        {
            Customer newCustomer = Instantiate(customerPrefab, transform);
            newCustomer.Initialize(
                customerImages[Random.Range(0, customerImages.Count)],
                itemRecipes[Random.Range(0, itemRecipes.Count)]);
        }

        private void OnDestroy()
        {
            CustomerController.OnCustomerNeeded -= AddCustomer;
            
        }
        
      /*  private void Save(List<StoredItem> playerGridContents)
        {
            contents = playerGridContents;
            string path = Path.Combine( Application.persistentDataPath, "inventory.json");
            File.WriteAllText(path, JsonUtility.ToJson(this));
        }

        private void Load()
        {
            string path = Path.Combine( Application.persistentDataPath, "inventory.json");
            if (!File.Exists(path)) return;
        
            JsonUtility.FromJsonOverwrite(File.ReadAllText(path), this);
        }*/
      
    }
}