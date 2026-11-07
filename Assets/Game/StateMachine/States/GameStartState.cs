using Game.Scripts;
using Game.StateMachine.States;
using UnityEngine;

public class GameStartState : GameState
{
    [Header("Parametri di Attesa")]
    [SerializeField] private float waitDuration = 3f;
    
    private float timer;

    public override void OnEnter()
    {
        EventManager.Instance.OnGameEventTriggered.AddListener((string message) =>
        {
            Debug.Log(message);
        });
        
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
        timer += Time.deltaTime;
    }

    public override bool IsTransitionConditionMet()
    {
        return timer >= waitDuration;
    }

    public override void OnExit()
    {
        base.OnExit();
        Debug.Log($"[Stato: {stateName}] Uscito dallo stato.");
    }
}