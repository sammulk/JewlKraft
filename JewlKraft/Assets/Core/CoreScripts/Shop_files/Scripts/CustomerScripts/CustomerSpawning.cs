using System;
using Core.CoreScripts.Inventory_files.Scripts.Databases;
using Core.Shop_files.Scripts.CustomerScripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

namespace Core.CoreScripts.Shop_files.Scripts.CustomerScripts
{
    [Serializable]
    [RequireComponent(typeof(SpriteLibrary))]
    public class CustomerSpawning : MonoBehaviour
    {
        [SerializeField] private GameObject _exclamationMarkPF;
        [SerializeField] private CraftingRecipe recipe;
        [SerializeField] private int _daysRemaining;

        private SpriteLibrary _spriteLibrary;
        private Purchasing _purchasing;
        private CustomerInteract _customerInteract;

        private void Awake()
        {
            _spriteLibrary = gameObject.GetComponent<SpriteLibrary>();
            _purchasing = GetComponentInChildren<Purchasing>();
            _customerInteract = GetComponent<CustomerInteract>();
        }

        public void FromSaveData(CustomerData customerData)
        {
            if (_spriteLibrary != null) _spriteLibrary.spriteLibraryAsset = customerData.SpriteLibrary;
            else Debug.LogError("Sprite Library is null");

            recipe = customerData.Recipe;
            _purchasing.SetRecipe(recipe);
            _customerInteract.SetRecipe(recipe);
            
            _daysRemaining = customerData.daysRemaining;
            CheckLastDayIndicator();

            _purchasing.OnDesireAcquired += ForwardLeaveRequest;
        }

        private void ForwardLeaveRequest()
        {
            CustomerController.Instance.currentManager.RemoveCustomer(this);
        }
        
        public void LeaveAndDelete(Transform spawnTransform)
        {
            _purchasing.gameObject.SetActive(false);
            GetComponent<CustomerInteract>().enabled = false;
            GetComponent<CustomerWanderer>().FinalVoyage(spawnTransform);
        }

        public bool DayPassTimeOut()
        {
            _daysRemaining--;
            CheckLastDayIndicator();
            return _daysRemaining > 0;
        }

        private void CheckLastDayIndicator()
        {
            if (_daysRemaining == 1) Instantiate(_exclamationMarkPF, transform);
        }

        #region Save/Load

        public CustomerData ToSaveData()
        {
            return new CustomerData(
                GetSpriteID, 
                recipe, 
                _daysRemaining);
        }

        public int GetSpriteID => CustomerDatabase.Instance.GetId(_spriteLibrary.spriteLibraryAsset);

        #endregion
    }
}