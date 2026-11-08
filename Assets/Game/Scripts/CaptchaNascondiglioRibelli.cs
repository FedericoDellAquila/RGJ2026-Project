using UnityEngine;
using UnityEngine.InputSystem;

public class CaptchaNascondiglioRibelli : Captcha {
    private GameInput gameInput;
    [SerializeField] private GameObject indicatore;
    private bool indicatorePosizionato=false;

    private void Awake() {
        gameInput = Player.Instance.GetComponentInChildren<GameInput>();
    }
    private void OnEnable() {
        
        gameInput.Onclick += GameInput_Onclick;
    }
    private void OnDisable() {
        
        if (gameInput != null) {
            gameInput.Onclick -= GameInput_Onclick;
        }
    }
    private void GameInput_Onclick(object sender, System.EventArgs e) {
        Vector2 posizioneSchermo = Vector2.zero;

        if (Mouse.current != null) {
            posizioneSchermo = Mouse.current.position.ReadValue();
        }
        Vector3 posizioneMondo = Camera.main.ScreenToWorldPoint(posizioneSchermo);
        posizioneMondo.z = 0f;

            indicatore.transform.position = posizioneMondo;
            Debug.Log("Oggetto spawnato in posizione: " + posizioneMondo);
            indicatorePosizionato = true;
        
    }

    public override bool AreConditionsSatisfied() {
        return indicatorePosizionato;
    }

    
}

       

