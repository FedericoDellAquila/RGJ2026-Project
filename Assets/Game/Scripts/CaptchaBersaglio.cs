using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CaptchaBersaglio : Captcha {
    [SerializeField] private Button button;
    [SerializeField] private Animator animator;
    private GameInput gameInput; 

    private RectTransform rectBersaglio;
    private Canvas canvas;
    private bool bersaglioColpito = false;
    private int statoIniziale;

    private void Awake() {
        rectBersaglio = button.GetComponent<RectTransform>();
        canvas = button.GetComponentInParent<Canvas>();
        gameInput = Player.Instance.GetComponentInChildren<GameInput>();
    }

    private void Start() {
        if (animator != null)
            statoIniziale = animator.GetCurrentAnimatorStateInfo(0).fullPathHash;
    }

    private void OnEnable() {
        if (gameInput != null) gameInput.Onclick += GameInput_Onclick;
    }

    private void OnDisable() {
        if (gameInput != null) gameInput.Onclick -= GameInput_Onclick;
    }

    private void GameInput_Onclick(object sender, EventArgs e) {
        if (bersaglioColpito || Mouse.current == null) return;

        Camera cam = null;
        if (canvas.renderMode != RenderMode.ScreenSpaceOverlay)
            cam = canvas.worldCamera != null ? canvas.worldCamera : Camera.main;

        Vector2 posMouse = Mouse.current.position.ReadValue();
        if (RectTransformUtility.RectangleContainsScreenPoint(rectBersaglio, posMouse, cam))
            BersaglioColpito();
    }

    public void BersaglioColpito() {
        Debug.Log("bersaglioColpito!");
        bersaglioColpito = true;
    }

    public override bool AreConditionsSatisfied() {
        return bersaglioColpito;
    }

    public override void ResetToInitialState() {
        bersaglioColpito = false;
        if (animator != null) {
            animator.Play(statoIniziale, 0, 0f);
            animator.Update(0f);
        }
    }
}