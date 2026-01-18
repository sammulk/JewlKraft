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
        [SerializeField] 
        private Transform shopCounterTransform;
        
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
            var toRemove = _customers
                .Where(customer => !customer.DayPassTimeOut())
                .ToList();

            foreach (var customer in toRemove)
            {
                RemoveCustomer(customer);
            }

        }

        public void RemoveCustomer(CustomerSpawning customer)
        {
            _customers.Remove(customer);
            customer.LeaveAndDelete(spawnTransform);
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
            Vector2 spawnLocation = spawnTransform.position;
            if (!isNew) spawnLocation = PositionWithOffSet(
                saveTransforms[
                    Random.Range(0, saveTransforms.Count)]
                    .position, 
                0.6f);
            
            if (data.Recipe == null)
            {
                Debug.LogWarning("Recipe is null");
                return;
            }
            
            CustomerSpawning newCustomerSpawning = Instantiate(
                customerSpawningPrefab,
                spawnLocation, 
                Quaternion.identity
                );
            
            newCustomerSpawning.FromSaveData(data);
            newCustomerSpawning.gameObject.name = data.SpriteLibrary.name;
            
            _customers.Add(newCustomerSpawning);
            
            if (isNew)
            {
                CustomerWanderer wanderer = newCustomerSpawning.GetComponent<CustomerWanderer>();
                wanderer.SetTarget(shopCounterTransform.position);
            }
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

        private Vector3 PositionWithOffSet(Vector3 position, float offset)
        {
            float offsetX = offset * Random.Range(-1, 2);
            float offsetY = offset * Random.Range(-1, 2);
            
            return new Vector3(position.x + offsetX, position.y + offsetY);
        }
    }

    [Serializable]
    public class CustomerSaveData
    {
        public List<CustomerData> customers;
    }
}