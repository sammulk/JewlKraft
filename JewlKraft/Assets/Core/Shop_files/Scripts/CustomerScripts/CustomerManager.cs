using System;
using System.Collections.Generic;
using System.Linq;
using Core.Inventory_files.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Shop_files.Scripts.CustomerScripts
{
    public class CustomerManager : MonoBehaviour, ISaveable<CustomerSaveData>
    {
        [SerializeField] private Customer customerPrefab;

        [Header("Spawn variables")] 
        
        [SerializeField]
        private List<Sprite> customerImages = new();
        [SerializeField] 
        private List<CraftingRecipe> itemRecipes = new();
        
        private readonly List<Customer> _customers = new();
        
        void Start()
        {
            FromSaveData(CustomerController.Instance.customers);
            CustomerController.Instance.RegisterManager(this);
        }

        public void RequestCustomer()
        {
            UpdateDaysRemaining();
            AddCustomer();
        }

        private void UpdateDaysRemaining()
        {
            for (int i = _customers.Count - 1; i >= 0; i--)
            {
                var customer = _customers[i];
                if (!customer.DayPassTimeOut())
                {
                    Destroy(customer.gameObject);
                    _customers.RemoveAt(i);
                }
            }
        }

        private void AddCustomer()
        {
            Customer newCustomer = Instantiate(customerPrefab, transform);
            newCustomer.Initialize(
                customerImages[Random.Range(0, customerImages.Count)],
                itemRecipes[Random.Range(0, itemRecipes.Count)], 
                3);
            
            _customers.Add(newCustomer);
        }

        private void AddCustomer(CustomerData data)
        {
            Customer newCustomer = Instantiate(customerPrefab, transform);
            newCustomer.Initialize(
                data.image,
                data.recipe,
                data.daysRemaining);
            
            _customers.Add(newCustomer);
        }

        private void OnDestroy()
        {
            CustomerController.Instance.currentManager = null;
            CustomerController.Instance.customers = ToSaveData();
        }
        
        public CustomerSaveData ToSaveData()
        {
            return new CustomerSaveData
            {
                customers = _customers
                    .Select(c => c.ToSaveData())
                    .ToList()
            };
        }

        public void FromSaveData(CustomerSaveData data)
        {
            data.customers.ForEach(AddCustomer);
        }
    }
    
    [Serializable]
    public class CustomerSaveData
    {
        public List<CustomerData> customers;
    }

}