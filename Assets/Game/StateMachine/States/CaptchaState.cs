using Game.StateMachine.States;
using UnityEngine;

public class CaptchaState : GameState
{
    public override bool IsTransitionConditionMet()
    {
        return false;
    }
}
