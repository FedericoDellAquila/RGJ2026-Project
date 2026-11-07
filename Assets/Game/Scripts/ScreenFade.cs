using System;
using UnityEngine;

public class ScreenFade : MonoBehaviour
{
    [SerializeField] private float duration = 3.0f;
    private float _currentDuration;

    private void Awake()
    {
        _currentDuration = duration;
    }

    private void Update()
    {
        if (_currentDuration <= 0)
        {
            
        }
    }
}
