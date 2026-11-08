using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CaptchaPianetaMigliore : Captcha {
    [SerializeField] private Button areaMarte;
    private GameInput gameInput;

    private RectTransform rectMarte;
    private Canvas canvas;
    private bool marteCliccato = false;

    private void Awake() {
        rectMarte = areaMarte.GetComponent<RectTransform>();
        canvas = areaMarte.GetComponentInParent<Canvas>();
        gameInput = Player.Instance.GetComponentInChildren<GameInput>();
    }

    private void OnEnable() {
        if (gameInput != null) gameInput.Onclick += GameInput_Onclick;
    }

    private void OnDisable() {
        if (gameInput != null) gameInput.Onclick -= GameInput_Onclick;
    }

    private void GameInput_Onclick(object sender, EventArgs e) {
        if (marteCliccato || Mouse.current == null) return;

        if (ClickDentroCerchio(rectMarte, Mouse.current.position.ReadValue())) {
            Debug.Log("Marte cliccato!");
            marteCliccato = true;
        }
    }

    private bool ClickDentroCerchio(RectTransform rt, Vector2 posSchermo) {
        Camera cam = null;
        if (canvas.renderMode != RenderMode.ScreenSpaceOverlay)
            cam = canvas.worldCamera != null ? canvas.worldCamera : Camera.main;

        // Posizione del mouse nelle coordinate locali del RectTransform
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, posSchermo, cam, out Vector2 locale))
            return false;

        // Distanza dal centro, normalizzata sui due raggi (funziona anche con un'ellisse)
        Vector2 d = locale - rt.rect.center;
        float rx = rt.rect.width / 2f;
        float ry = rt.rect.height / 2f;
        return (d.x * d.x) / (rx * rx) + (d.y * d.y) / (ry * ry) <= 1f;
    }

    public override bool AreConditionsSatisfied() {
        return marteCliccato;
    }

    public override void ResetToInitialState() {
        marteCliccato = false;
    }
}