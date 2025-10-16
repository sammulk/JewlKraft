using UnityEngine;

public class GemCrop : MonoBehaviour
{
    [SerializeField] private GemCropData data;
    private int hitsToBreak;
    private int currentHits = 0;
    private Sprite gemCropSprite;
    private Sprite shardSprite;
    private GameObject dustPrefab;
    private GameObject shardPrefab;
    private int shardCount;

    private void Awake()
    {
        setData(data);
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
        Instantiate(dustPrefab, transform.position, Quaternion.Euler(180f, 0f, 0f));
    }

    private void SpawnShards()
    {
        if (shardPrefab == null) return;

        for (int i = 0; i < shardCount; i++)
        {
            GameObject shard = Instantiate(shardPrefab, transform.position, Quaternion.identity);

            SpriteRenderer sr = shard.GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.sprite = shardSprite;

            Rigidbody2D rb = shard.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 forceDir = Random.insideUnitCircle.normalized;
                rb.AddForce(forceDir * 5f, ForceMode2D.Impulse);
            }
        }
    }

    private void setData(GemCropData data)
    {
        hitsToBreak = data.HitsToMine;
        shardCount = data.ShardCount;
        shardSprite = data.GemType.gemSprite;
        gemCropSprite = data.GemCropSprite;
        shardPrefab = data.GemPrefab;
        dustPrefab = data.DustPrefab;
    }
}
