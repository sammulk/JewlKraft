    using Core.Inventory_files.Scripts.ItemScripts;
    using Core.Player_files.Scripts;
    using System;
    using UnityEngine;
    using UnityEngine.Serialization;

    namespace Core.Dungeon_files.Scripts
    {
        [RequireComponent(typeof(SpriteRenderer))]
        [RequireComponent(typeof(Rigidbody2D))]
        public class GemShard : MonoBehaviour
        {
            public static event Action<GemShard> OnGemShardPickedUp;

            [SerializeField] private AudioClip[] pickupSounds;
    
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
                if (component == null) return;

                AudioClip clip = pickupSounds[UnityEngine.Random.Range(0, pickupSounds.Length)];
                if (clip == null) return;

                // Plays the clip at the gems position without needing an AudioSource component
                AudioSource.PlayClipAtPoint(clip, transform.position, 1f);

                OnGemShardPickedUp?.Invoke(this);

                Destroy(gameObject);
            }

        }
    }
