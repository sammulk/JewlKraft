using System;
using Core.Inventory_files.Scripts;
using Core.Inventory_files.Scripts.ItemScripts;
using Core.Player_files.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Dungeon_files.Scripts
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class GemShard : MonoBehaviour
    {
        public static event Action<GemShard> OnGemShardPickedUp;
    
        [FormerlySerializedAs("gemData")] [HideInInspector]
        public ItemData itemData;
    
        public void Initialize(ItemData itemData)
        {
            this.itemData = itemData;
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.sprite = itemData.sprite; 
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            PlayerMovement component = other.gameObject.GetComponent<PlayerMovement>();
            if (!component) return;
        
            OnGemShardPickedUp?.Invoke(this);
        }
    }
}
