using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CaptchaManager : MonoBehaviour
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
        captchaWindow.SetRenderLayer(1);
        
        for (uint i = 0; i < count; ++i)
            CreateCaptcha(captchaWindow);

        return captchaWindow;
    }
    
    private void CreateCaptcha(CaptchaWindow captchaWindow)
    {
        List<Captcha> selectedCaptchas = new List<Captcha>();
        
        int randomIndex = Random.Range(0, availableCaptchas.Count);
        while (randomIndex == _lastRandomIndex)
            randomIndex = Random.Range(0, availableCaptchas.Count);
        
        GameObject selectedCaptchaObject = Instantiate(availableCaptchas[randomIndex]);
        _lastRandomIndex = randomIndex;
        
        selectedCaptchas.Add(selectedCaptchaObject.GetComponent<Captcha>());
        
        captchaWindow.AssignCaptchaList(selectedCaptchas);
    }

    public void ClearCaptchas()
    {
        foreach (GameObject captchaWindow in _captchas)
            Destroy(captchaWindow);
        _captchas.Clear();
    }
}
