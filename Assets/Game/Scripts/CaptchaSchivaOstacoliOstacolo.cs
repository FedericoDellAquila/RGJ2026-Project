using UnityEngine;

public class CaptchaSchivaOstacoliOstacolo : MonoBehaviour {
    private Rigidbody2D rb;
    private Vector2 direzione = Vector2.left;
    [SerializeField] private float velocita = 2f;
    [SerializeField] private CaptchaSchivaOstacoli captchaSchivaOstacoli;

    private bool puòMuoversi;
    
    
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        puòMuoversi = true;
    }

   
    void FixedUpdate() {
        if (rb != null && puòMuoversi && captchaSchivaOstacoli.getStoGiocando() == true) {
            rb.linearVelocity = new Vector2(direzione.x * velocita, rb.linearVelocity.y);
        }
    }

    
    
}
