using System.Collections;
using Game.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Singleton<Player>
{
    public int startingHealth = 3;
    private int _currentHealth;
    [SerializeField] private Camera playerCamera;
    
    private Vector2 worldMousePosition;

    protected override void Awake()
    {
        _currentHealth = startingHealth;
        
        EventManager.Instance.OnPlayerDamageNotificationEnd.AddListener(OnGameOverCallback);
    }
    
    void Update()
    {
        UpdateWorldMousePosition();
    }

    public int GetHealth()
    {
        return _currentHealth;
    }
    
    private void UpdateWorldMousePosition()
    {
        if (playerCamera == null)
            return;
        
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        worldMousePosition = playerCamera.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, 0.0f));
    }

    public Vector2 GetMouseWorldLocation()
    {
        return worldMousePosition;
    }

    public void TakeDamage()
    {
        Camera.main.GetComponent<ScreenShake>().Shake();
        _currentHealth--;
        EventManager.Instance.TriggerPlayerDamageNotificationStart(_currentHealth);
    }

    private void OnGameOverCallback(int value)
    {
        if (_currentHealth <= 0)
            EventManager.Instance.TriggerGameOver();
    }

    private IEnumerator ShowHealth()
    {
        Debug.Log("Inizio attesa...");
        
        // Attende per 2 secondi
        yield return new WaitForSeconds(2f);
        
        Debug.Log("Sono passati 2 secondi!");
    }
}
