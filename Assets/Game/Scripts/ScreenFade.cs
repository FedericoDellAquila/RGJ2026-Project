using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    [SerializeField] private Image blackScreen;
    [SerializeField] private float duration = 3.0f;
    private float _currentDuration;
    private bool _isFading;
    private readonly Color _blackColor = Color.black;
    private readonly Color _transparentColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);

    public UnityEvent OnScreenFadeFinished = new UnityEvent();

    private void Start()
    {
        StartFading();
    }

    private void Update()
    {
        if (_isFading == false)
            return;

        _currentDuration -= Time.deltaTime;
        if (_currentDuration <= 0)
        {
            _isFading = false;
            blackScreen.color = _transparentColor;
            OnScreenFadeFinished.Invoke();
            return;
        }

        float normalizedValue = _currentDuration / duration;
        blackScreen.color = Color.Lerp(_transparentColor, _blackColor, normalizedValue);
    }

    public void StartFading()
    {
        blackScreen.color = _blackColor;
        
        _currentDuration = duration;
        _isFading = true;
    }
}
