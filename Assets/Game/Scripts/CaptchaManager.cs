using System;
using System.Collections.Generic;
using Game.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

public class CaptchaManager : Singleton<CaptchaManager>
{
    [SerializeField] private GameObject captchaWindowPrefab;
    [SerializeField] private List<GameObject> availableCaptchas;
    
    private List<GameObject> _captchas;

    private int _lastRandomIndex = -1;

    private void Start()
    {
        CreateCaptchaWindow(3);
    }

    public CaptchaWindow CreateCaptchaWindow(int count)
    {
        GameObject captchaWindowObject = Instantiate(captchaWindowPrefab);
        CaptchaWindow captchaWindow = captchaWindowObject.GetComponent<CaptchaWindow>();
        captchaWindow.Show();
        return captchaWindow;
    }

    public Captcha CreateCaptcha()
    {
        int randomIndex = Random.Range(0, availableCaptchas.Count);
        while (randomIndex == _lastRandomIndex)
            randomIndex = Random.Range(0, availableCaptchas.Count);
        
        GameObject selectedCaptchaObject = Instantiate(availableCaptchas[randomIndex]);
        _lastRandomIndex = randomIndex;
        
        return selectedCaptchaObject.GetComponent<Captcha>();
    }

    public void ClearCaptchas()
    {
        foreach (GameObject captchaWindow in _captchas)
            Destroy(captchaWindow);
        _captchas.Clear();
    }
}
