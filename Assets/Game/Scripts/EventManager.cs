using System;
using UnityEngine.Events;

namespace Game.Scripts
{
    public class EventManager : Singleton<EventManager>
    {
        // Esempio 1: Evento C# standard (consigliato per performance e pulizia)
        public event Action<int> OnScoreChanged;

        // Esempio 2: UnityEvent statico (se preferisci gestirlo tramite firme standard)
        public UnityEvent<string> OnGameEventTriggered = new UnityEvent<string>();

        // Metodi per invocare gli eventi in sicurezza
        public void TriggerScoreChange(int newScore)
        {
            OnScoreChanged?.Invoke(newScore);
        }

        public void TriggerGameEvent(string message)
        {
            OnGameEventTriggered?.Invoke(message);
        }
    }
}
