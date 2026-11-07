using UnityEngine;

public class CaptchaSchivaOstacoli : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform ostacolo;
    private Vector2 direzione = Vector2.left;
    private float velocità = 5f;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ostacolo.position = velocità * direzione;
    }
}
