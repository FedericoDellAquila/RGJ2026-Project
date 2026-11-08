using Game.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaptchaSchivaOstacoli : Captcha {
    [SerializeField] private CaptchaSchivaOstacoliPlayer player;
    private Animator animator;
    

    private Rigidbody2D rbPlayer;
    private GameInput gameInput;

    [Header("Impostazioni Salto")]
    [SerializeField] private float forzaSaltoPlayer = 7f;
    private bool aTerra = true;
    private bool stoGiocando = true;
    [SerializeField] Transform[] ostacoli;
    private bool playerStaBene = true;
    Vector3 posizionePartenzaOstacolo1 = new Vector3(15.06f, -1.835f, 0);
    Vector3 posizionePartenzaOstacolo2 = new Vector3(2.92f, -1.835f, 0);

    private void Start() {

        posizionePartenzaOstacolo1 = ostacoli[0].transform.position;
        posizionePartenzaOstacolo2 = ostacoli[1].transform.position;
        playerStaBene = true;
    }

    private void Awake() {
        // Trova il GameInput
        animator = player.GetComponent<Animator>();
        if (Player.Instance != null) {
            gameInput = Player.Instance.GetComponentInChildren<GameInput>();
        }

        if (player != null) {
            rbPlayer = player.GetComponent<Rigidbody2D>();


            player.OnCollisionEntered += Player_OnCollisionEntered;

        }

        if (gameInput != null) {
            gameInput.Onclick += GameInput_Onclick;
        }

        EventManager.Instance.OnPlayerDamageNotificationEnd.AddListener(PartenzaGioco);
    }

    private void OnDestroy() {

        if (gameInput != null) gameInput.Onclick -= GameInput_Onclick;
        if (player != null) {
            player.OnCollisionEntered -= Player_OnCollisionEntered;
            
        }
    }

    private void GameInput_Onclick(object sender, System.EventArgs e) {
        if (aTerra && rbPlayer != null && stoGiocando) {
            Salta();
        }
    }

    private void Salta() {
        animator.SetBool("isJumping",true);
        rbPlayer.linearVelocity = new Vector2(rbPlayer.linearVelocity.x, forzaSaltoPlayer);
        aTerra = false;
    }

    // Gestisce il contatto con il pavimento
    private void Player_OnCollisionEntered(object sender, Collision2D collision) {
        if (collision.gameObject.name.Contains("pavimento")) {
            aTerra = true;
            animator.SetBool("isJumping", false);
        }
        if (collision.gameObject.name.Contains("Ostacolo") && stoGiocando == true) {
            Debug.Log("Game Over! Ostacolo colpito.");
            stoGiocando = false;
            playerStaBene = false;
            foreach (Transform ostacolo in ostacoli) {
                ostacolo.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            }
        }
    }
    public void PartenzaGioco(int i) {
        ResetToInitialState();
        stoGiocando = true;
    }

    public bool getStoGiocando() {
        return stoGiocando;
    }

    public override bool AreConditionsSatisfied() {
        return playerStaBene;
    }

    public override void ResetToInitialState() {
        ostacoli[0].position = posizionePartenzaOstacolo1;
        ostacoli[1].position = posizionePartenzaOstacolo2;
        playerStaBene = true;
        foreach (Transform ostacolo in ostacoli) {
            ostacolo.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }
    }
 
}



