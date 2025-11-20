using UnityEngine;
using System;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    public static event Action OnTimeRanOut;

    [SerializeField]
    private float maxTime = 10;

    [SerializeField]
    private Image EnergyBar;

    private float _currentTime = 0;

    private void Start()
    {
        _currentTime = maxTime;

        if (EnergyBar != null)
        {
            EnergyBar.fillAmount = Mathf.Clamp01(_currentTime / Mathf.Max(0.0001f, maxTime));
            if (EnergyBar.fillAmount <= 0f)
            {
                OnTimeRanOut?.Invoke();
            }
        }
    }

    private void Update()
    {
        if (_currentTime > 0f)
        {
            _currentTime -= Time.deltaTime;
            _currentTime = Mathf.Max(0f, _currentTime);
        }

        if (EnergyBar != null)
        {
            EnergyBar.fillAmount = Mathf.Clamp01(_currentTime / Mathf.Max(0.0001f, maxTime));
        }

        if (_currentTime <= 0f)
        {
            OnTimeRanOut?.Invoke();
        }
    }
}