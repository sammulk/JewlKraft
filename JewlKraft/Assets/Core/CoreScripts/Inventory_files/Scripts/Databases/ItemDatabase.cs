using System.Collections.Generic;
using Core.Inventory_files.Scripts.ItemScripts;
using UnityEngine;
using static Core.Inventory_files.Scripts.ItemScripts.Helpers;

namespace Core.CoreScripts.Inventory_files.Scripts.Databases
{
    [CreateAssetMenu(menuName = "Game/Item Database")]
    public class ItemDatabase : ScriptableObject
    {
        private static ItemDatabase _instance;

        public static ItemDatabase Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<ItemDatabase>("ItemDatabase");
                    _instance.BuildLookup();
                }

                return _instance;
            }
        }

        [SerializeField] private List<ItemData> RawItems;
        [SerializeField] private List<ItemData> ProcessedItems;
        [SerializeField] private List<ItemData> CompletedItems;
        
        private Dictionary<(MaterialType, CraftStage), ItemData> _lookup;
        private Dictionary<(MaterialType, ItemType), ItemData> _completeItemLookup;

        private void BuildLookup()
        {
            _lookup = new();
            _completeItemLookup = new Dictionary<(MaterialType, ItemType), ItemData>();
            
            AddLookupsOfType(RawItems, CraftStage.Raw);
            AddLookupsOfType(ProcessedItems, CraftStage.Processed);
            AddLookupsOfType(CompletedItems, CraftStage.Ready) ;
        }

        private void AddLookupsOfType(List<ItemData> items, CraftStage craftStage)
        {
            foreach (ItemData entry in items)
            {
                if (entry.CraftStage != craftStage)
                {
                    Debug.LogWarning($"{entry.name} " +
                                     $"has craftStage {entry.CraftStage.ToString()} " +
                                     $"instead of {craftStage.ToString()}");
                }

                if (craftStage == CraftStage.Ready)
                {
                    if (entry.ItemType == ItemType.Undefined) Debug.LogWarning($"{entry.name}, {entry.MaterialType} has itemtype {entry.ItemType}");
                    
                    var key = (entry.MaterialType, entry.ItemType);
                    if (!_completeItemLookup.TryAdd(key, entry)) Debug.LogError($"Duplicate item for {key}");
                    continue;
                }
                AddLookup(entry);
            }
        }

        private void AddLookup(ItemData entry)
        {
            var key = (entry.MaterialType, entry.CraftStage);
            if (!_lookup.TryAdd(key, entry)) Debug.LogError($"Duplicate item for {key}");
        }

        public ItemData Get(ItemLookup lookupData)
        {
            if (lookupData.CraftStage == CraftStage.Ready)
            {
                return _completeItemLookup[(lookupData.MaterialType, lookupData.ItemType)];
            }
            return _lookup[(lookupData.MaterialType, lookupData.CraftStage)];
        }
    }

    [System.Serializable]
    public struct ItemLookup
    {
        public MaterialType MaterialType;
        public CraftStage CraftStage;
        public ItemType ItemType;
        
        public ItemLookup(MaterialType materialType, CraftStage craftStage, ItemType itemType)
        {
            MaterialType = materialType;
            CraftStage = craftStage;
            ItemType = itemType;
        }

        public ItemLookup(ItemData itemData)
        {
            MaterialType = itemData.MaterialType;
            CraftStage = itemData.CraftStage;
            ItemType = itemData.ItemType;
            
            Debug.Log($"New itemdata with name {itemData.name} MaterialType {MaterialType} and CraftStage {CraftStage}");
        }
        
        public bool CompareToLookup(ItemLookup itemLookup)
        {
            if (itemLookup.ItemType != ItemType) return false;
            if (itemLookup.MaterialType != MaterialType) return false;
            if (itemLookup.CraftStage != CraftStage) return false;
            
            return true;
        }
    }
}