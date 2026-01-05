using Core.Inventory_files.Scripts.ItemScripts;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;

namespace Core.Dungeon_files.Scripts
{
    public class GemCrop : MonoBehaviour
    {
        [SerializeField] private GemCropData data;
        [SerializeField] private GemShard shardPrefab;

        [SerializeField] private AudioClip[] hitSounds;
        [SerializeField, Range(0.5f, 1.5f)] private float pitchMin = 0.95f;
        [SerializeField, Range(0.5f, 1.5f)] private float pitchMax = 1.05f;

        private AudioSource audioSource;
        [SerializeField] private AudioMixerGroup caveMixerGroup;

        private GameObject dustPrefab;
        private Color gemDustColor;
        private int hitsToBreak;
        private int currentHits = 0;
        private Sprite gemCropSprite;
        private ItemData _itemData;
        private int shardCount;

        private void Awake()
        {
            // Load data and spirte
            SetData(data);
            SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.sprite = gemCropSprite;

            // Add shine
            Transform shine = transform.Find("Shine");
            Light2D shineColour = shine.GetComponent<Light2D>();
            shineColour.color = data.itemType.color;

            // Audio stuff
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = false;
            audioSource.playOnAwake = false;
            audioSource.outputAudioMixerGroup = caveMixerGroup;
        }
        public void OnHit()
        {
            currentHits++;
            SpawnDust();

            // Choose random clip
            AudioClip clip = hitSounds[Random.Range(0, hitSounds.Length)];
            if (clip == null) return;

            audioSource.pitch = Random.Range(pitchMin, pitchMax);

            if (currentHits >= hitsToBreak)
            {
                AudioSource temp = new GameObject("FinalHit").AddComponent<AudioSource>();
                temp.clip = clip;
                temp.volume = 2f;
                temp.spatialBlend = 0f; // 2D
                temp.Play();
                Destroy(temp.gameObject, clip.length);
                BreakGem();
                return;
            }
            audioSource.PlayOneShot(clip);
        }

        private void BreakGem()
        {
            SpawnShards();
            Destroy(gameObject);
        }

        private void SpawnDust()
        {
            if (dustPrefab == null) return;
            GameObject dust = Instantiate(dustPrefab, transform.position, Quaternion.Euler(180f, 0f, 0f));
            ParticleSystem ps = dust.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                var main = ps.main;
                main.startColor = new ParticleSystem.MinMaxGradient(
                    gemDustColor * 0.8f,
                    gemDustColor * 1.2f
                );
            }
        }

        private void SpawnShards()
        {
            if (shardPrefab == null) return;

            for (int i = 0; i < shardCount; i++)
            {
                GemShard shard = Instantiate(shardPrefab, transform.position, Quaternion.identity);

                shard.Initialize(_itemData);
            
                Rigidbody2D rb = shard.GetComponent<Rigidbody2D>();
                if (rb == null) continue;
            
                Vector2 forceDir = Random.insideUnitCircle.normalized;
                rb.AddForce(forceDir * 5f, ForceMode2D.Impulse);
            }
        }

        private void SetData(GemCropData cropData)
        {
            hitsToBreak = cropData.HitsToMine;
            shardCount = cropData.ShardCount;
            _itemData = cropData.itemType;
            gemCropSprite = cropData.GemCropSprite;
            dustPrefab = cropData.DustPrefab;
            gemDustColor = cropData.itemType.color;
        }
    }
}
