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
        public static event Action<CraftingRecipe> OnRecipeSelected;

        [SerializeField] private GameObject _exclamationMarkPF;
        [SerializeField] private CraftingRecipe recipe;
        [SerializeField] private int _daysRemaining;

        private SpriteLibrary _spriteLibrary;

        private void Awake()
        {
            _spriteLibrary = gameObject.GetComponent<SpriteLibrary>();
        }

        public void FromSaveData(CustomerData customerData)
        {
            if (_spriteLibrary != null) _spriteLibrary.spriteLibraryAsset = customerData.SpriteLibrary;
            else Debug.LogError("Sprite Library is null");

            recipe = customerData.Recipe;
            _daysRemaining = customerData.daysRemaining;
            CheckLastDayIndicator();
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