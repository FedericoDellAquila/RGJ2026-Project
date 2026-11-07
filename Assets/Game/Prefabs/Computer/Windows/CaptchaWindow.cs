using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CaptchaWindow : MonoBehaviour
{
    public UnityEvent OnAllCaptchaCompleted;

    [SerializeField] private SpriteRenderer Image;
    [SerializeField] private SpriteRenderer Panel;
    [SerializeField] private TMP_Text Text;
    [SerializeField] private TimeBar timeBar;
    [SerializeField] private int numOfCaptcha = 3;
    private Captcha _currentCaptcha;

    private void Start()
    {
        if (numOfCaptcha <= 0)
        {
            MoveToNextCaptcha();
            return;
        }
        
        RequestCaptcha();
    }

    public void RequestCaptcha()
    {
        _currentCaptcha = CaptchaManager.Instance.CreateCaptcha();
        _currentCaptcha.transform.SetParent(transform);
        _currentCaptcha.gameObject.SetActive(true);
        _currentCaptcha.SetRenderLayer(2);
        
        timeBar.onTimeout.AddListener(OnTimeout);
        timeBar.timeLimit = _currentCaptcha.timeLimit;
        timeBar.Initialize();
        timeBar.StartTimer();
    }

    private void OnTimeout()
    {
        if (_currentCaptcha.AreConditionsSatisfied())
        {
            Destroy(_currentCaptcha);
            MoveToNextCaptcha();
        }
        else
        {
            _currentCaptcha.ResetToInitialState();
            timeBar.ResetTimer();
            timeBar.StartTimer();
        }
    }

    private void MoveToNextCaptcha()
    {
        numOfCaptcha--;

        if (numOfCaptcha <= 0)
        {
            OnAllCaptchaCompleted.Invoke();
            Destroy(gameObject);
            return;
        }
        
        RequestCaptcha();
    }
    
    public void Show()
    {
        Image.sortingOrder = -1;
        Panel.sortingOrder = -1;
        Text.GetComponent<MeshRenderer>().sortingOrder = -1;
    }

    private void OnDestroy()
    {
        OnAllCaptchaCompleted.RemoveAllListeners();
    }
}
