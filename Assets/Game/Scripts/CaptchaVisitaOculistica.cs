using TMPro;
using UnityEngine;

public class CaptchaVisitaOculistica : Captcha
{
    private string paroladaDaCopiare = "LPED";
    [SerializeField] TMP_InputField inputField;

    private string stringa;

    private void Start() {
        
        if (inputField != null) {
            inputField.Select();
            inputField.ActivateInputField();
        }
    }
    private void Update() {
        stringa = inputField.text;
    }

    public override bool AreConditionsSatisfied() {
        return stringa != null && stringa.Trim().ToUpper() == paroladaDaCopiare.ToUpper();
    }
    public override void ResetToInitialState() {
        stringa = "";
        inputField.text = "";
    }
}
