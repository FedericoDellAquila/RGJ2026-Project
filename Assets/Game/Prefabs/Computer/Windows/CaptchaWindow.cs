using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CaptchaWindow : MonoBehaviour
{
    public UnityEvent OnAllCaptchaCompleted;

    [SerializeField] private SpriteRenderer Image;
    [SerializeField] private SpriteRenderer Panel;
    [SerializeField] private TMP_Text Text;

    private int _currentCaptchaIndex;
    private List<Captcha> _captchas;
    
    public void AssignCaptchaList(List<Captcha> captchaList)
    {
        _captchas = captchaList;

        foreach (Captcha captcha in captchaList)
        {
            captcha.transform.SetParent(Image.transform);
            captcha.gameObject.SetActive(false);
        }

        _currentCaptchaIndex = _captchas.Count - 1;

        Captcha lastCaptcha = _captchas[_currentCaptchaIndex];
        lastCaptcha.gameObject.SetActive(true);
        lastCaptcha.
        
        lastCaptcha.OnComplete.AddListener(OnCaptchaCompleted);
    }

    private void OnCaptchaCompleted(Captcha captcha)
    {
        captcha.OnComplete.RemoveListener(OnCaptchaCompleted);
        _captchas.Remove(captcha);
        Destroy(captcha);

        MoveToNextCaptcha();
    }

    private void MoveToNextCaptcha()
    {
        if (_captchas.Count != 0)
            return;

        OnAllCaptchaCompleted.Invoke();
        Destroy(this);
    }
    
    public void SetRenderLayer(uint layer)
    {
        Image.renderingLayerMask = layer;
        Panel.renderingLayerMask = layer;
        Text.gameObject.layer = (int)layer;
    }

    private void OnDestroy()
    {
        if (_captchas == null)
            return;
        
        foreach (Captcha captcha in _captchas)
            captcha.OnComplete.RemoveListener(OnCaptchaCompleted);
        
        OnAllCaptchaCompleted.RemoveAllListeners();
    }
}
