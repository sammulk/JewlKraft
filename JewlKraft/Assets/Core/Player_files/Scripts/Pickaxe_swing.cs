using Core.Dungeon_files.Scripts;
using UnityEngine;

public class PickaxeSwing : MonoBehaviour
{
    [SerializeField] private float swingRange = 3.5f;
    [SerializeField] private float swingCooldown = 0.4f;
    [SerializeField] private LayerMask gemLayer;
    [SerializeField] private GameObject pickaxeObj;

    private float nextSwingTime = 0f;

    private void Start()
    {
        pickaxeObj.SetActive(false);
    }

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

    private void SwingPickaxe()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, swingRange, gemLayer);

        if (hit.collider != null)
        {
            GemCrop gem = hit.collider.GetComponent<GemCrop>();
            if (gem != null)
            {
                gem.OnHit();
            }
        }

        StartCoroutine(ShowPickaxeVisual(direction));
    }

    private System.Collections.IEnumerator ShowPickaxeVisual(Vector2 direction)
    {
        if (pickaxeObj == null) yield break;

        pickaxeObj.SetActive(true);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        pickaxeObj.transform.localPosition = direction * 0.5f; // offset from player
        pickaxeObj.transform.localRotation = Quaternion.Euler(0f, 0f, angle);

        yield return new WaitForSeconds(0.2f);

        pickaxeObj.SetActive(false);
    }
}
