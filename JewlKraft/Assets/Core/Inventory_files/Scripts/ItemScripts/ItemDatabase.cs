using System.Collections.Generic;
using UnityEngine;
using static Core.Inventory_files.Scripts.ItemScripts.Helpers;

namespace Core.Inventory_files.Scripts.ItemScripts
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

        private void BuildLookup()
        {
            _lookup = new();
            
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
                AddLookup(entry);
            }
        }

        private void AddLookup(ItemData entry)
        {
            var key = (entry.MaterialType, entry.CraftStage);
            if (!_lookup.TryAdd(key, entry)) Debug.LogError($"Duplicate item for {key}");
        }

        public ItemData Get(MaterialType mat, CraftStage stage)
        {
            return _lookup[(mat, stage)];
        }
    }
}