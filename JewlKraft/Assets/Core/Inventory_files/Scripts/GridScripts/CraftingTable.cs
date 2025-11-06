using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Inventory_files.Scripts.GridScripts
{
    [RequireComponent(typeof(CraftingSetup))]
    public class CraftingTable : MonoBehaviour
    {
        public static event Action<InventoryItem> OnCraftingComplete;
         
        private List<VerifyContent> _children = new ();
        private GemData _rewardItem;

        public void SetupReady(GemData reward)
        {
            _children = GetComponentsInChildren<VerifyContent>().ToList();
            foreach (VerifyContent child in _children)
            {
                child.OnCorrectContents += ChildGotCorrectContents;
            }
            _rewardItem = reward;
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
