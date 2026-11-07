using Game.Scripts;
using Game.StateMachine.States;
using UnityEngine;

public class GameStartState : GameState
{
    public override void OnEnter()
    {
        base.OnEnter();

        Player.Instance.GetComponent<MouseToggle>().SetMouseEnabled(false);
        
        Camera.main.GetComponent<ScreenFade>().OnScreenFadeFinished.AddListener(OnScreenFadeFinishedCallback);
    }

    private void OnScreenFadeFinishedCallback()
    {
        Player.Instance.GetComponent<MouseToggle>().SetMouseEnabled(true);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override bool IsTransitionConditionMet()
    {
        return EventManager.Instance.documentationStarted == true;
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}