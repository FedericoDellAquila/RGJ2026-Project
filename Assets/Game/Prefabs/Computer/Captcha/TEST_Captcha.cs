using System;
using UnityEngine;

public class TEST_Captcha : Captcha
{
    [SerializeField] private WorldButton Button;

    private bool _isPressed;
    
    private void Awake()
    {
        Button.OnButtonClicked.AddListener(() =>
        {
            Debug.Log("DONE");
            _isPressed = true;
        });
    }

    public override void SetRenderLayer(int layer)
    {
        Button.GetComponent<SpriteRenderer>().sortingOrder = 3;
    }

    public override bool AreConditionsSatisfied() 
    {
        return _isPressed;
    }
}
