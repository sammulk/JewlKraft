using Core.Inventory_files.Scripts;
using UnityEngine;

namespace Core.Dungeon_files.Scripts
{
    public class GemCrop : MonoBehaviour
    {
        [SerializeField] private GemCropData data;
        [SerializeField] private GemShard shardPrefab;

        private GameObject dustPrefab;
        private Color gemDustColor;
        private int hitsToBreak;
        private int currentHits = 0;
        private Sprite gemCropSprite;
        private GemData gemData;
        private int shardCount;

        private void Awake()
        {
            SetData(data);
            SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.sprite = gemCropSprite;
        }
        public void OnHit()
        {
            currentHits++;
            SpawnDust();

            if (currentHits >= hitsToBreak)
            {
                BreakGem();
            }
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

                shard.Initialize(gemData);
            
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
            gemData = cropData.GemType;
            gemCropSprite = cropData.GemCropSprite;
            dustPrefab = cropData.DustPrefab;
            gemDustColor = cropData.GemType.gemColor;
        }
    }
}
