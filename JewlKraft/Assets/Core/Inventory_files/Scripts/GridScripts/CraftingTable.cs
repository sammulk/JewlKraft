using System;
using System.Collections.Generic;
using System.Linq;
using Core.Inventory_files.Scripts.ItemScripts;
using UnityEngine;

namespace Core.Inventory_files.Scripts.GridScripts
{
    [RequireComponent(typeof(CraftingSetup))]
    public class CraftingTable : MonoBehaviour
    {
        public static event Action<InventoryItem> OnCraftingComplete;
         
        private List<VerifyContent> _children = new ();
        private ItemData _rewardItem;

        public void SetupReady(ItemData reward)
        {
            _children = GetComponentsInChildren<VerifyContent>().ToList();
            foreach (VerifyContent child in _children)
            {
                child.OnCorrectContents += ChildGotCorrectContents;
            }
            _rewardItem = reward;
        }

        public void Cleanup()
        {
            foreach (VerifyContent child in _children)
            {
                child.OnCorrectContents -= ChildGotCorrectContents;
            }
            _rewardItem = null;
            _children.Clear();
        }

        private void ChildGotCorrectContents()
        {
            if (_children.All(child => child.CompareContents()))
            {
                Debug.Log("You win");
                InventoryItem newItem = InventoryFactory.Instance.Create(_rewardItem);
                OnCraftingComplete?.Invoke(newItem);
                gameObject.SetActive(false);
            }
        }
    }
}
