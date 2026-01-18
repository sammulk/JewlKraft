using Core.Dungeon_files.Scripts;
using Core.Player_files.Scripts;
using TMPro;
using UnityEngine;

public class PickaxeSwing : MonoBehaviour
{
    [SerializeField] private float swingRange = 3.5f;
    [SerializeField] private float swingCooldown = 1.2f;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask gemLayer;
    [SerializeField] private LayerMask breakableWallLayer;

    [Header("References")]
    [SerializeField] private BreakableWallController breakableWallController;
    [SerializeField] private InventoryButton invButton;
    [SerializeField] private ESC_Panel escPanel;

    private float nextSwingTime = 0f;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextSwingTime && invButton.isOpen == false && escPanel.isOpen == false)
        {
            PlayerMovement player = GetComponent<PlayerMovement>();

            if (player != null)
            {
                StartCoroutine(player.Trapped(swingCooldown));
                animator.SetTrigger("Swing");
                nextSwingTime = Time.time + swingCooldown;
            }
        }
    }

    private void SwingPickaxe()
    {
        float dirX = animator.GetFloat("LastInputX");
        float dirY = animator.GetFloat("LastInputY");

        Vector2 direction = new Vector2(dirX, dirY).normalized;
        if (direction == Vector2.zero)
            direction = Vector2.down;

        // First check crops
        RaycastHit2D hitGem = Physics2D.Raycast(transform.position, direction, swingRange, gemLayer);
        if (hitGem.collider != null)
        {
            GemCrop gem = hitGem.collider.GetComponent<GemCrop>();
            if (gem != null)
            {
                gem.OnHit();
                return; // stop here, don't also break walls
            }
        }

        // Second check walls
        RaycastHit2D hitWall = Physics2D.Raycast(transform.position, direction, swingRange, breakableWallLayer);
        if (hitWall.collider != null)
        {
            if (breakableWallController != null)
            {
                breakableWallController.HitTile(hitWall.point);
            }
        }
    }
}
