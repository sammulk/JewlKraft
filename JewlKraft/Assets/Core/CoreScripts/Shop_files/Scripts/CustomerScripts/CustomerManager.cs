using System;
using System.Collections.Generic;
using System.Linq;
using Core.Inventory_files.Scripts;
using Core.Shop_files.Scripts.CustomerScripts;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Core.CoreScripts.Shop_files.Scripts.CustomerScripts
{
    public class CustomerManager : MonoBehaviour, ISaveable<CustomerSaveData>
    {
        [FormerlySerializedAs("customerInteractPrefab")] [FormerlySerializedAs("customerPrefab")] [SerializeField] private CustomerSpawning customerSpawningPrefab;

        [Header("Spawn variables")] 
        
        [SerializeField] 
        private List<CraftingRecipe> itemRecipes = new();

        [SerializeField] 
        private List<Transform> saveTransforms = new();
        
        [SerializeField] 
        private Transform spawnTransform;
        
        private readonly List<CustomerSpawning> _customers = new();
        
        void Start()
        {
            FromSaveData(CustomerController.Instance.Customers);
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
            CustomerDatabase customerDatabase = CustomerDatabase.Instance;

            int lastCustomerIndex = (_customers.Count == 0) ? -1 : _customers[^1].GetSpriteID;
            int customerIndex = (lastCustomerIndex + 1) % customerDatabase.GetCount();

            CustomerData newData = new CustomerData(
                customerIndex, 
                itemRecipes[Random.Range(0, itemRecipes.Count)]);
            
            AddCustomer(newData, true);
        }

        private void AddCustomer(CustomerData data, bool isNew = false)
        {
            Transform usedTransform = spawnTransform;
            if (!isNew) usedTransform = saveTransforms[Random.Range(0, saveTransforms.Count)];
            
            if (data.Recipe == null)
            {
                Debug.LogWarning("Recipe is null");
                return;
            }
            
            CustomerSpawning newCustomerSpawning = Instantiate(
                customerSpawningPrefab,
                PositionWithOffSet(usedTransform.position, 0.7f), 
                usedTransform.rotation
                );
            
            newCustomerSpawning.FromSaveData(data);
            newCustomerSpawning.gameObject.name = data.SpriteLibrary.name;
            
            _customers.Add(newCustomerSpawning);
        }

        private void OnDestroy()
        {
            CustomerController.Instance.currentManager = null;
            CustomerController.Instance.Customers = ToSaveData();
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
            data.customers.ForEach(customerData => AddCustomer(customerData));
        }

        private Vector3 PositionWithOffSet(Vector3 position, float maxOffset)
        {
            float offsetX = position.x + Random.Range(-maxOffset, maxOffset);
            float offsetY = position.y + Random.Range(-maxOffset, maxOffset);
            
            return new Vector3(position.x + offsetX, position.y + offsetY);
        }
    }

    [Serializable]
    public class CustomerSaveData
    {
        public List<CustomerData> customers;
    }
}