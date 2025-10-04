using System;
using UnityEngine;

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
        Destroy(gameObject);
    }
}
