using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class TimeBar : MonoBehaviour
{
    public UnityEvent onTimeout = new UnityEvent();
    
    public float timeLimit;
    private float _currentTime;
    public bool isTimerStarted;
    
    [SerializeField] protected GameObject pivotPoint;
    
    public void SetNormalizedProgress(float value)
    {
        float clampedValue = Math.Clamp(value, 0.0f, 1.0f);
        
        pivotPoint.transform.localScale = new Vector3(
            clampedValue, 
            pivotPoint.transform.localScale.y, 
            pivotPoint.transform.localScale.z
        );
        
        Debug.Log("ClampedValue: " + clampedValue);
        Debug.Log("localScale: " + pivotPoint.transform.localScale.x);
    }

    public void Initialize()
    {
        _currentTime = timeLimit;
    }

    private void Update()
    {
        if (isTimerStarted)
        {
            _currentTime -= Time.deltaTime;
        
            if (_currentTime <= 0.0f)
            {
                onTimeout.Invoke();
                SetNormalizedProgress(0.0f);
            }
            else
            {
                SetNormalizedProgress(_currentTime / timeLimit);
            }
        }
    }

    public void StartTimer()
    {
        isTimerStarted = true;
    }
    
    public void ResetTimer()
    {
        _currentTime = timeLimit;
        isTimerStarted = false;
    }
}
