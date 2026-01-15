using System.Collections.Generic;
using Core.Inventory_files.Scripts.ItemScripts;
using UnityEngine;
using UnityEngine.U2D.Animation;

namespace Core.CoreScripts.Shop_files.Scripts.CustomerScripts
{
    [CreateAssetMenu(menuName = "Game/CustomerDatabase")]
    public class CustomerDatabase : ScriptableObject
    {
        private static CustomerDatabase _instance;

        public static CustomerDatabase Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<CustomerDatabase>("CustomerDatabase");
                    _instance.BuildLookup();
                }

                return _instance;
            }
        }

        [SerializeField] private List<SpriteLibraryAsset> _spriteLibraries;

        private Dictionary<int, SpriteLibraryAsset> _lookup;
        private Dictionary<SpriteLibraryAsset, int> _reverseLookup;

        public int GetCount() => _spriteLibraries.Count;
        
        public int GetId(SpriteLibraryAsset spriteLibraryAsset)
        {
            return _reverseLookup[spriteLibraryAsset];
        }
        
        public SpriteLibraryAsset GetAsset(int id)
        {
            return _lookup[id];
        }

        private void BuildLookup()
        {
            _lookup = new();
            _reverseLookup = new();

            for (var i = 0; i < _spriteLibraries.Count; i++)
            {
                SpriteLibraryAsset entry = _spriteLibraries[i];
                AddLookup(i, entry);
            }
        }

        private void AddLookup(int key, SpriteLibraryAsset entry)
        {
            if (!_lookup.TryAdd(key, entry)) Debug.LogError($"Duplicate item for {entry.name}");
            if (!_reverseLookup.TryAdd(entry, key)) Debug.LogError($"Duplicate item for {entry.name}");
        }
    }
}