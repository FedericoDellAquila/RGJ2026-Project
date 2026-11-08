using UnityEngine;
using UnityEngine.UI;

public class CaptchaPremiRipetutamente : Captcha
{
    [SerializeField] private Slider barraSlider;
    private float targetProgress = 0f;

    [SerializeField] private float velocit‡Salita = 5f;
    [SerializeField] private float velocit‡Discesa = 0.4f;
    [SerializeField] private float incrementoPerClic = 0.15f;
    GameInput gameInput;
    private bool barraRiempita = false;
    [SerializeField] private GameObject palloncino1;
    [SerializeField] private GameObject palloncino2;
    [SerializeField] private GameObject palloncino3;
    [SerializeField] private GameObject palloncino4;
    private void Awake() {
        gameInput = Player.Instance.GetComponentInChildren<GameInput>();
        gameInput.Onclick += GameInput_Onclick;
    }

    private void GameInput_Onclick(object sender, System.EventArgs e) {
        AggiungiProgresso();
    }

    private void Start() {
        barraSlider.value = 0f;
        targetProgress = 0f;
        palloncino1.SetActive(true);
        palloncino2.SetActive(false);
        palloncino3.SetActive(false);
        palloncino4.SetActive(false);


    }

    private void Update() {
        targetProgress -= velocit‡Discesa * Time.deltaTime;
        targetProgress = Mathf.Clamp01(targetProgress);

        if(barraSlider.value != targetProgress) {
            barraSlider.value = Mathf.MoveTowards(barraSlider.value, targetProgress, velocit‡Salita * Time.deltaTime);
        }
        if(barraSlider.value >= 0 && barraSlider.value <= 0.3) {
            palloncino1.SetActive(true);
            palloncino2.SetActive(false);
            palloncino3.SetActive(false);
            palloncino4.SetActive(false);
        }
        else if(barraSlider.value > 0.3 && barraSlider.value <= 0.6) {
                palloncino1.SetActive(false);
                palloncino2.SetActive(true);
                palloncino3.SetActive(false);
                palloncino4.SetActive(false);
            } 
        else if (barraSlider.value > 0.6 && barraSlider.value <= 0.85) {
            palloncino1.SetActive(false);
            palloncino2.SetActive(false);
            palloncino3.SetActive(true);
            palloncino4.SetActive(false);
        } 
        else if (barraSlider.value > 0.85 && barraSlider.value <= 1) {
            palloncino1.SetActive(false);
            palloncino2.SetActive(false);
            palloncino3.SetActive(false);
            palloncino4.SetActive(true);
        }


        if (barraSlider.value >= 0.90) {
            barraRiempita = true;
        } else {
            barraRiempita = false;
        }
        
    }

    private void AggiungiProgresso() {
        targetProgress += incrementoPerClic;
        targetProgress = Mathf.Clamp01(targetProgress);
    }

    public override bool AreConditionsSatisfied() {
        return barraRiempita;
    }

    public override void ResetToInitialState() {
        barraSlider.value = 0f;
        targetProgress = 0f;
        barraRiempita = false;
}
}
