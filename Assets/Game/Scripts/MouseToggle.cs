using UnityEngine;
using UnityEngine.EventSystems;

public class MouseToggle : MonoBehaviour
{
    [Tooltip("Start with the mouse disabled?")]
    public bool startDisabled = true;

    [Tooltip("Also disable the EventSystem so UI can't react to anything")]
    public bool disableEventSystem = true;

    private bool _mouseEnabled;
    private EventSystem _eventSystem;
    
    void Awake()
    {
        // Cache it now, while it's still active and "current"
        _eventSystem = EventSystem.current;
        if (_eventSystem == null)
            _eventSystem = FindFirstObjectByType<EventSystem>(FindObjectsInactive.Include);
    }

    public void SetMouseEnabled(bool enabled)
    {
        _mouseEnabled = enabled;

        Cursor.visible = enabled;
        Cursor.lockState = enabled ? CursorLockMode.None : CursorLockMode.Locked;

        if (disableEventSystem)
        {
            // Re-find if the cached one was destroyed (e.g. scene reload)
            if (_eventSystem == null)
                _eventSystem = FindFirstObjectByType<EventSystem>(FindObjectsInactive.Include);

            if (_eventSystem != null)
                _eventSystem.enabled = enabled;
        }
    }
}
