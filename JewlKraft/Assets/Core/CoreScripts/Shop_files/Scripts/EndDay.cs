using UnityEngine;
using UnityEngine.SceneManagement;

public class EndDay : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        FadeController.Instance.FadeToScene("Dungeon_scene");
    }
}
