using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Dungeon_files.Scripts
{
    public class Death : MonoBehaviour
    {
        private void OnEnable()
        {
            TimeCounter.OnTimeRanOut += PlayerDeath;
        }

        private void OnDisable()
        {
            TimeCounter.OnTimeRanOut -= PlayerDeath;
        }

        private void PlayerDeath()
        {
            SceneManager.LoadScene("Shop_scene");
        }
    }
}
