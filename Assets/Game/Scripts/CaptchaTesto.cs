using System;
using System.Collections.Generic;
using Game.Scripts;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class CaptchaTesto : Captcha
{
    [SerializeField] TextMeshProUGUI parolaDaCopiare;
    [SerializeField] TMP_InputField inputField;

    [SerializeField] private List<string> words = new List<string>();

    private int _lastIndex = -1;
    
    private string stringa;
    private string parolaChiave = "Ciao";

    void Start()
    {
        PickRandomWord();
        EventManager.Instance.OnPlayerDamageNotificationEnd.AddListener(OnPlayerDamageNotificationEndCallback);
    }

    private void Update()
    {
        stringa = inputField.text;
    }

    private void OnPlayerDamageNotificationEndCallback(int value)
    {
        PickRandomWord();
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

    private void PickRandomWord()
    {
        int randomIndex = Random.Range(0, words.Count);
        
        while (randomIndex == _lastIndex)
            randomIndex = Random.Range(0, words.Count);

        _lastIndex = randomIndex;
        parolaChiave = words[randomIndex];

        if (parolaDaCopiare != null)
            parolaDaCopiare.text = parolaChiave;
    }
}

    