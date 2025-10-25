using System;
using Core.Inventory_files.Scripts;
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
    
        [HideInInspector]
        public GemData gemData;
    
        public void Initialize(GemData gemData)
        {
            this.gemData = gemData;
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.sprite = gemData.gemSprite; 
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            PlayerMovement component = other.gameObject.GetComponent<PlayerMovement>();
            if (!component) return;
        
            OnGemShardPickedUp?.Invoke(this);
        }
    }
}
