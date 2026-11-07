using System;
using UnityEngine;
using UnityEngine.Events;

public class Captcha : MonoBehaviour
{
    public float timeLimit = 3.0f;
    // private float _currentTime;
    // private bool _isTimerStarted;
    
    public UnityEvent<Captcha> OnComplete;
    public UnityEvent<Captcha> OnFailed;

    public Bounds bounds;

    private void Awake()
    {
        // _currentTime = timeLimit;
    }

    private void Update()
    {
        // if (_isTimerStarted)
        // {
        //     _currentTime -= Time.deltaTime;
        //
        //     if (_currentTime <= 0.0f)
        //     {
        //         if (AreConditionsSatisfied())
        //             OnComplete.Invoke(this);
        //         else
        //             OnFailed.Invoke(this);
        //         
        //         ResetTimer();
        //     }
        // }
    }

    public void ResetToInitialState()
    {
        ResetTimer();
    }

    public void GetNormalizedTimeValue()
    {
        
    }

    public void StartTimer()
    {
        // _isTimerStarted = true;
    }
    
    public void ResetTimer()
    {
        // _currentTime = timeLimit;
        // _isTimerStarted = false;
    }

    public virtual bool AreConditionsSatisfied()
    {
        return false;
    }
    
    public virtual void SetRenderLayer(int layer)
    {
    }
}
