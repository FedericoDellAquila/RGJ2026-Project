using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions inputActions;
    public event EventHandler Onsalto;
    private void Awake() {
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();
        inputActions.Player.Click.performed += Click_performed;

    }

    private void Click_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        Onsalto?.Invoke(this, EventArgs.Empty);
    }

   
}
