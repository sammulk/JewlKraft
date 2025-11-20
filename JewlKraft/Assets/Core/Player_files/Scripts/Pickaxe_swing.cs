using Core.Dungeon_files.Scripts;
using Core.Player_files.Scripts;
using UnityEngine;

public class PickaxeSwing : MonoBehaviour
{
    [SerializeField] private float swingRange = 3.5f;
    [SerializeField] private float swingCooldown = 1.2f;
    [SerializeField] private LayerMask gemLayer;

    private float nextSwingTime = 0f;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextSwingTime)
        {
            PlayerMovement player = GetComponent<PlayerMovement>();

            if (player != null)
            {
                // Prevent movement briefly
                StartCoroutine(player.Trapped(swingCooldown));
                // Trigger the directional swing animation
                animator.SetTrigger("Swing");
                // Set cooldown
                nextSwingTime = Time.time + swingCooldown;
            }
        }
    }

    private void SwingPickaxe()
    {
        // Get the last facing direction from the animator
        float dirX = animator.GetFloat("LastInputX");
        float dirY = animator.GetFloat("LastInputY");

        Vector2 direction = new Vector2(dirX, dirY).normalized;
        if (direction == Vector2.zero)
            direction = Vector2.down; // fallback direction if none stored

        // Raycast in that direction
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
