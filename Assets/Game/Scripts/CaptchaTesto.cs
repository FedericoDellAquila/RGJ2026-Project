using TMPro;
using UnityEngine;

public class CaptchaTesto : Captcha
{
    [SerializeField] TextMeshProUGUI parolaDaCopiare;
    [SerializeField] TMP_InputField inputField;

    private string stringa;
    private string parolaChiave = "Ciao";

    void Start()
    {
        if (parolaDaCopiare != null)
        {
            parolaDaCopiare.text = parolaChiave;
        }
    }

    private void Update()
    {
        stringa = inputField.text;
    }


    public override bool AreConditionsSatisfied()
    {
        return stringa != null && stringa.Trim() == parolaChiave;
    }

    public override void ResetToInitialState()
    {
        stringa = "";
        inputField.text = "";
    }
}

    