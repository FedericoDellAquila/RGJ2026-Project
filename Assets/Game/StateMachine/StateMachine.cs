using System.Collections.Generic;
using Game.StateMachine.States;
using UnityEngine;

namespace Game.StateMachine
{
    public class StateMachine : MonoBehaviour
    {
        [Header("Sequenza di Stati (Classi C#)")]
        // [SerializeReference] permette di serializzare classi polimorfiche nell'Inspector
        [SerializeReference]
        private List<GameState> sequenceStates = new List<GameState>();

        [SerializeField] private bool loopSequence = false;

        private int currentIndex = 0;
        private GameState currentState;
        private bool isRunning = false;

        private void Start()
        {
            if (sequenceStates != null && sequenceStates.Count > 0)
            {
                StartStateMachine();
            }
            else
            {
                Debug.LogWarning("Nessun stato configurato nella State Machine!", this);
            }
        }

        private void Update()
        {
            if (!isRunning || currentState == null) return;

            // Esegue la logica dello stato corrente
            currentState.OnUpdate();

            // Controlla se la condizione di transizione è soddisfatta
            if (currentState.IsTransitionConditionMet())
            {
                GoToNextState();
            }
        }

        public void StartStateMachine()
        {
            currentIndex = 0;
            ChangeState(currentIndex);
            isRunning = true;
        }

        private void GoToNextState()
        {
            currentIndex++;

            if (currentIndex < sequenceStates.Count)
            {
                ChangeState(currentIndex);
            }
            else
            {
                if (loopSequence)
                {
                    currentIndex = 0;
                    ChangeState(currentIndex);
                }
                else
                {
                    isRunning = false;
                    currentState?.OnExit();
                    currentState = null;
                    Debug.Log("Sequenza della State Machine completata.", this);
                }
            }
        }

        private void ChangeState(int index)
        {
            currentState?.OnExit();
            currentState = sequenceStates[index];
            currentState?.OnEnter();
        }
    }
}