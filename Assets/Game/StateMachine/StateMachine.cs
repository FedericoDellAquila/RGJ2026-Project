using System.Collections.Generic;
using System.ComponentModel;
using Game.Scripts;
using Game.StateMachine.States;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.StateMachine
{
    public class StateMachine : MonoBehaviour
    {
        [Header("Sequenza di Stati (Classi C#)")]
        // [SerializeReference] permette di serializzare classi polimorfiche nell'Inspector
        [SerializeReference] private List<GameState> sequenceStates = new List<GameState>();
        [SerializeField] private GameState gameOverState;
        
        [SerializeField] private bool loopSequence = false;
        
        private int _currentIndex = 0;
        [SerializeField, ReadOnly(true)] private GameState currentState;
        private bool _isRunning = false;

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
            
            EventManager.Instance.OnGameOver.AddListener(OnGameOverCallback);
        }

        private void OnGameOverCallback()
        {
            SceneManager.LoadScene(0);
        }

        private void Update()
        {
            if (!_isRunning || currentState == null) return;

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
            _currentIndex = 0;
            ChangeState(_currentIndex);
            _isRunning = true;
        }

        private void GoToNextState()
        {
            _currentIndex++;

            if (_currentIndex < sequenceStates.Count)
            {
                ChangeState(_currentIndex);
            }
            else
            {
                if (loopSequence)
                {
                    _currentIndex = 0;
                    ChangeState(_currentIndex);
                }
                else
                {
                    _isRunning = false;
                    currentState?.OnExit();
                    currentState = null;
                    Debug.Log("Sequenza della State Machine completata.", this);
                }
            }
        }

        private void ChangeState(int index)
        {
            if (currentState != null)
            {
                currentState.OnExit();
                Destroy(currentState.gameObject);
            }
            
            currentState = Instantiate(sequenceStates[index], transform);
            currentState?.OnEnter();
        }
    }
}