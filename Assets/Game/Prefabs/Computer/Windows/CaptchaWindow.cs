using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts;
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
    [SerializeField] private float pausaTraCaptcha = 2f; // secondi di pausa
    public int numOfCaptcha = 3;
    private Captcha _currentCaptcha;

    private void Start()
    {
        timeBar.onTimeout.AddListener(OnTimeout);
        
        EventManager.Instance.OnPlayerDamageNotificationStart.AddListener(OnPlayerDamageNotificationStartCallback);
        EventManager.Instance.OnPlayerDamageNotificationEnd.AddListener(OnPlayerDamageNotificationEndCallback);
        
        if (numOfCaptcha <= 0)
        {
            MoveToNextCaptcha();
            return;
        }
        
        RequestCaptcha();
    }

    public int GetNumOfCaptcha()
    {
        return numOfCaptcha;
    }

    private void OnPlayerDamageNotificationStartCallback(int value)
    {
        timeBar.ResetTimer();
    }
    
    private void OnPlayerDamageNotificationEndCallback(int value)
    {
        _currentCaptcha.ResetToInitialState();
        timeBar.StartTimer();
    }

    public void RequestCaptcha()
    {
        _currentCaptcha = CaptchaManager.Instance.CreateCaptcha();
        _currentCaptcha.transform.SetParent(transform);
        _currentCaptcha.gameObject.SetActive(true);
        _currentCaptcha.SetRenderLayer(2);
        
        timeBar.timeLimit = _currentCaptcha.timeLimit;
        timeBar.Initialize();
        timeBar.StartTimer();
    }

    private void OnTimeout()
    {
        if (_currentCaptcha.AreConditionsSatisfied())
        {
            StartCoroutine(Wait());
        }
        else
        {
            Player.Instance.TakeDamage();
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(_currentCaptcha.gameObject);
        _currentCaptcha = null;
        if (numOfCaptcha > 1)                                                       //
            yield return new WaitForSeconds(pausaTraCaptcha);                       //
        MoveToNextCaptcha();
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
