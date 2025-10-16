using UnityEngine;
using UnityEngine.SceneManagement;

public class EndDay : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene("Dungeon_scene");
    }
    //TODO If walked onto bed teleport to cave

}
