using UnityEngine;

public class EndDay : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        GetComponent<TutorialBubble>()?.HideBubble();
        FadeController.Instance.FadeToScene("Dungeon_scene");
    }
}
