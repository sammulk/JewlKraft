using System;
using Core.Shop_files.Scripts.CustomerScripts;
using UnityEngine;

namespace Core.Inventory_files.Scripts
{
    public class PersistentLoader : MonoBehaviour
    {
        public static PersistentLoader Instance { get; private set; }

        public GameSave SaveInfo { get; private set; }

        private PlayerInventory inventory;


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
        }

        private void OnEnable()
        {
            LoadGame();
        }
        private void LoadGame()
        {
            SaveInfo = SaveManager.LoadGame();
            inventory = Resources.Load("PlayerInventory") as PlayerInventory;
            System.Diagnostics.Debug.Assert(inventory != null, nameof(inventory) + " != null");

            if (inventory.contents.Count > 0)
            {
                //Debug.LogWarning("Inventory already changed, will not load");
                //return;
            }
            inventory.FromSaveData(SaveInfo.playerInventory);
        }

        private void SaveGame()
        {
            Debug.Log("Saving game");
            CustomerController controller = CustomerController.Instance;
            CustomerSaveData customers = controller.currentManager == null ? controller.Customers : controller.currentManager.ToSaveData();
            if (customers == null) Debug.LogWarning("No Customers!");
            
            GameSave save = new GameSave
            {
                playerInventory = inventory.ToSaveData(),
                customers = customers
            };

            SaveManager.SaveGame(save);
        }

        private void OnDestroy()
        {
            if (Instance == this) SaveGame();
        }
    }
}