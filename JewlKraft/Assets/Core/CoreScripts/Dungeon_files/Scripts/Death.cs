using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Dungeon_files.Scripts
{
    public class Death : MonoBehaviour
    {
        private void OnEnable()
        {
            //Debug.Log("Death subscribed to OnTimeRanOut");
            TimeCounter.OnTimeRanOut += PlayerDeath;
        }

        private void OnDisable()
        {
            //Debug.Log("Death unsubscribed from OnTimeRanOut");
            TimeCounter.OnTimeRanOut -= PlayerDeath;
        }

        private void PlayerDeath()
        {
            //Debug.Log("Player has died. Loading Shop_scene.");
            SceneManager.LoadScene("Shop_scene");
        }
    }
}
