using Game.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Singleton<Player>
{
    [SerializeField] private Camera playerCamera;
    
    private Vector3 worldMousePosition;
    
    void Update()
    {
        UpdateWorldMousePosition();
    }

    private void UpdateWorldMousePosition()
    {
        if (playerCamera == null)
            return;
        
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        worldMousePosition = playerCamera.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, -playerCamera.transform.position.z));
        worldMousePosition.z = 0.0f;
    }
}
