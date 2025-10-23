using Core.Dungeon_files.Scripts;
using UnityEngine;

public class PickaxeSwing : MonoBehaviour
{
    [SerializeField] private float swingRange = 3.5f;
    [SerializeField] private float swingCooldown = 0.4f;
    [SerializeField] private LayerMask gemLayer;

    private float nextSwingTime = 0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextSwingTime)
        {
            PlayerMovement player = GetComponent<PlayerMovement>();

            if (player != null)
            {
                StartCoroutine(player.Trapped(0.1f));
                SwingPickaxe();
                nextSwingTime = Time.time + swingCooldown;
            }
        }
    }

    void SwingPickaxe()
    {
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, swingRange, gemLayer);

        if (hit.collider != null)
        {
            GemCrop gem = hit.collider.GetComponent<GemCrop>();
            if (gem != null)
            {
                gem.OnHit();
            }
        }
    }
}
