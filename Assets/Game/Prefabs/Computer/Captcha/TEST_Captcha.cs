using System;
using UnityEngine;

public class TEST_Captcha : Captcha
{
    [SerializeField] private WorldButton Button;

    private void Awake()
    {
        Button.OnButtonClicked.AddListener(() =>
        {
            Debug.Log("DONE");
            OnComplete.Invoke(this);
        });
    }
}
