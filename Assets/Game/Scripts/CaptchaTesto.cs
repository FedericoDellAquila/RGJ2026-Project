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
        if(parolaDaCopiare != null) {
            parolaDaCopiare.text = parolaChiave;
        }

        if (inputField != null) {
            inputField.onSubmit.AddListener(CheckWord);
        }

}
    public override bool AreConditionsSatisfied() {
        return stringa.Trim() == parolaChiave;
    }
    
   



    void CheckWord(string testo) { 
            stringa = testo;
        }
    }

    