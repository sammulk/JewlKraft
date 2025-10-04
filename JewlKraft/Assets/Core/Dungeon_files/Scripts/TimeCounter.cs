using System;
using TMPro;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    public static event Action OnTimeRanOut;

    public static void InvokeTimeRanOut()
    {
        OnTimeRanOut?.Invoke();
    }
    
    [SerializeField] 
    private float maxTime = 69;
    
    private float _currentTime = 0;
    private TextMeshProUGUI _timerText;
    
    void Awake()
    {
        _timerText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _currentTime = maxTime;
    }

    private void Update()
    {
        _currentTime -= Time.deltaTime;
        _timerText.text = _currentTime.ToString();
        if (_currentTime <= 0)
        {
            OnTimeRanOut?.Invoke();
        }
    }
}
