using UnityEngine;

namespace Game.StateMachine.States
{
    public abstract class GameState : MonoBehaviour
    {
        public string stateName;

        // Chiamato quando si entra nello stato
        public virtual void OnEnter() { }

        // Chiamato ogni Update finché lo stato è attivo
        public virtual void OnUpdate() { }

        // Chiamato quando si esce dallo stato
        public virtual void OnExit() { }

        // Condizione automatica per passare al prossimo stato
        public abstract bool IsTransitionConditionMet();
    }
}
