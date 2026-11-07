using UnityEngine;
using UnityEngine.SceneManagement;

public class CaptchaSchivaOstacoli : Captcha {
    [SerializeField] private CaptchaSchivaOstacoliPlayer player;
    

    private Rigidbody2D rbPlayer;
    private GameInput gameInput;

    [Header("Impostazioni Salto")]
    [SerializeField] private float forzaSaltoPlayer = 7f;
    private bool aTerra = true;

    private void Awake() {
        // Trova il GameInput
        if (Player.Instance != null) {
            gameInput = Player.Instance.GetComponentInChildren<GameInput>();
        }

        if (player != null) {
            rbPlayer = player.GetComponent<Rigidbody2D>();


            player.OnCollisionEntered += Player_OnCollisionEntered;

        }

        if (gameInput != null) {
            gameInput.Onsalto += GameInput_Onsalto;
        }
    }

    private void OnDestroy() {

        if (gameInput != null) gameInput.Onsalto -= GameInput_Onsalto;
        if (player != null) {
            player.OnCollisionEntered -= Player_OnCollisionEntered;
            
        }
    }

    private void GameInput_Onsalto(object sender, System.EventArgs e) {
        if (aTerra && rbPlayer != null) {
            Salta();
        }
    }

    private void Salta() {
        rbPlayer.linearVelocity = new Vector2(rbPlayer.linearVelocity.x, forzaSaltoPlayer);
        aTerra = false;
    }

    // Gestisce il contatto con il pavimento
    private void Player_OnCollisionEntered(object sender, Collision2D collision) {
        if (collision.gameObject.name.Contains("pavimento")) {
            aTerra = true;
        }
        if (collision.gameObject.name.Contains("Ostacolo")) {
            Debug.Log("Game Over! Ostacolo colpito.");
        }


    }
}



