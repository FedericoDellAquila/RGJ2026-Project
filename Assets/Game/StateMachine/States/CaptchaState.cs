using Game.StateMachine.States;
using UnityEngine;

public class CaptchaState : GameState
{
    private CaptchaWindow _captchaWindow;

    public override void OnEnter()
    {
        _captchaWindow = CaptchaManager.Instance.CreateCaptchaWindow(3);
    }
    
    public override bool IsTransitionConditionMet()
    {
        return _captchaWindow.GetNumOfCaptcha() <= 0;
    }
}
