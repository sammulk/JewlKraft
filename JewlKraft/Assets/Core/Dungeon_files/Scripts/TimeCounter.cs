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
    private bool _timeRanOutInvoked = false;

    private void Start()
    {
        _currentTime = maxTime;
        _timeRanOutInvoked = false;

        if (EnergyBar != null)
        {
            EnergyBar.fillAmount = Mathf.Clamp01(_currentTime / Mathf.Max(0.0001f, maxTime));
            if (EnergyBar.fillAmount <= 0f && !_timeRanOutInvoked)
            {
                _timeRanOutInvoked = true;
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

            if (EnergyBar.fillAmount <= 0f && !_timeRanOutInvoked)
            {
                _timeRanOutInvoked = true;
                OnTimeRanOut?.Invoke();
            }
        }
    }
}