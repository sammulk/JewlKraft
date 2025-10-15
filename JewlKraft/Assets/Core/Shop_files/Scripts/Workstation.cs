using UnityEngine;

public class Workstation : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color defaultColor;
    [SerializeField] private Color highlightColor = Color.white;
    private bool playerNearby = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
    }

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("In range");
            playerNearby = true;
            spriteRenderer.color = highlightColor;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Out of range");
            playerNearby = false;
            spriteRenderer.color = defaultColor;
        }
    }

    private void Interact()
    {
        // Siin teeb iga workstation erinevat asja
        Debug.Log("Interacted with " + gameObject.name);
    }
}
