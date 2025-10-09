using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene("Main_menu");
    }
}
