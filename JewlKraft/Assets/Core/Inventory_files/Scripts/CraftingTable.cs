using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Inventory_files.Scripts
{
    public class CraftingTable : MonoBehaviour
    {
        public static event Action<InventoryItem> OnCraftingComplete;
         
        private List<VerifyContent> _children = new ();

        private void Awake()
        {
            _children = GetComponentsInChildren<VerifyContent>().ToList();
        }

        private void OnEnable()
        {
            foreach (VerifyContent child in _children)
            {
                child.OnCorrectContents += ChildGotCorrectContents;
            }
        }

        private void ChildGotCorrectContents()
        {
            if (_children.All(child => child.CompareContents()))
            {
                Debug.Log("You win");
                GemData windata = Resources.Load<GemData>("WinAsset");
                InventoryItem newItem = InventoryFactory.Instance.Create(windata);
                OnCraftingComplete?.Invoke(newItem);
                gameObject.SetActive(false);
            }
        }
    }
}
