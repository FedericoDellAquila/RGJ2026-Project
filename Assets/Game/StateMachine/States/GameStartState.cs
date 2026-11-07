using Game.Scripts;
using Game.StateMachine.States;
using UnityEngine;

[CreateAssetMenu(fileName = "EventState", menuName = "StateMachine/States/GameStart")]
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
        timer = 0f;
        Debug.Log($"[Stato: {stateName}] Inizio attesa di {waitDuration} secondi.");
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