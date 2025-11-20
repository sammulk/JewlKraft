using System;
using System.Collections.Generic;
using Core.Inventory_files.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Core.Shop_files.Scripts.CustomerScripts
{
    public class CustomerController : MonoBehaviour
    {
        public static CustomerController Instance { get; private set; }

        [HideInInspector] public CustomerManager currentManager;

        [HideInInspector] private CustomerSaveData _customers;
        public CustomerSaveData Customers
        {
            get
            {
                if (_customers == null)
                {
                    _customers = PersistentLoader.Instance.SaveInfo.customers;
                }
                return _customers;
            }
            set => _customers = value;
        }

        private string _previousScene;
        private string _currentScene;
        private bool _addCustomerUnHandled = false;

        private void Awake()
        {
            // Singleton pattern
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            _currentScene = SceneManager.GetActiveScene().name;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy()
        {
            if (Instance != this) return;
            
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        
        /// <summary>
        /// Called by CustomerManager in Start!!.
        /// </summary>
        public void RegisterManager(CustomerManager manager)
        {
            currentManager = manager;

            if (_addCustomerUnHandled)
            {
                _addCustomerUnHandled = false;
                manager.RequestCustomer();
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            _previousScene = _currentScene;
            _currentScene = scene.name;

            HandleSceneTransition(_previousScene, _currentScene);
        }

        private void HandleSceneTransition(string fromScene, string toScene)
        {
            Debug.Log($"from {fromScene} to {toScene}");
            // spawn Customers only if coming from dungeon
            if (fromScene == "Dungeon_scene" && toScene == "Shop_scene")
            {
                _addCustomerUnHandled = true;
            }
        }
    }
}

