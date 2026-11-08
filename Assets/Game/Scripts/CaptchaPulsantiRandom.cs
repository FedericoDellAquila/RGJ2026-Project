using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CaptchaPulsantiRandom : Captcha {
    [SerializeField] private RectTransform areaSpawn;      // zona dove compaiono i pulsanti
    [SerializeField] private RectTransform prefabPulsante; // prefab UI (una semplice Image)
    private GameInput gameInput;
    [SerializeField] private int numeroPulsanti = 5;

    private readonly List<RectTransform> pulsantiAttivi = new List<RectTransform>();
    private Canvas canvas;
    private int pulsantiCliccati = 0;

    private void Awake() {
        gameInput = Player.Instance.GetComponentInChildren<GameInput>();
        canvas = areaSpawn.GetComponentInParent<Canvas>();
        if (gameInput == null)
            gameInput = FindFirstObjectByType<GameInput>();
    }

    private void OnEnable() {
        if (gameInput != null) gameInput.Onclick += GameInput_Onclick;
        SpawnTutti();
    }

    private void OnDisable() {
        if (gameInput != null) gameInput.Onclick -= GameInput_Onclick;
    }

    private void SpawnTutti() {
        for (int i = 0; i < numeroPulsanti; i++)
            SpawnPulsante();
    }

    private void SpawnPulsante() {
        RectTransform p = Instantiate(prefabPulsante, areaSpawn);

        Rect area = areaSpawn.rect;
        // dimensione reale del pulsante, scala compresa
        float w = p.rect.width * p.localScale.x;
        float h = p.rect.height * p.localScale.y;
        Vector2 pos = Vector2.zero;

        for (int tentativo = 0; tentativo < 30; tentativo++) {
            pos = new Vector2(
                UnityEngine.Random.Range(area.xMin + w / 2f, area.xMax - w / 2f),
                UnityEngine.Random.Range(area.yMin + h / 2f, area.yMax - h / 2f));

            if (!SiSovrappone(pos, w, h)) break;
        }

        p.localPosition = pos;
        pulsantiAttivi.Add(p);
    }

    private bool SiSovrappone(Vector2 pos, float w, float h) {
        Rect nuovo = new Rect(pos.x - w / 2f, pos.y - h / 2f, w, h);
        foreach (RectTransform altro in pulsantiAttivi) {
            float aw = altro.rect.width * altro.localScale.x;
            float ah = altro.rect.height * altro.localScale.y;
            Vector2 ap = altro.localPosition;
            Rect r = new Rect(ap.x - aw / 2f, ap.y - ah / 2f, aw, ah);
            if (nuovo.Overlaps(r)) return true;
        }
        return false;
    }

    private void GameInput_Onclick(object sender, EventArgs e) {
        if (Mouse.current == null) return;

        Camera cam = null;
        if (canvas.renderMode != RenderMode.ScreenSpaceOverlay)
            cam = canvas.worldCamera != null ? canvas.worldCamera : Camera.main;

        Vector2 posMouse = Mouse.current.position.ReadValue();

        for (int i = pulsantiAttivi.Count - 1; i >= 0; i--) {
            if (RectTransformUtility.RectangleContainsScreenPoint(pulsantiAttivi[i], posMouse, cam)) {
                Destroy(pulsantiAttivi[i].gameObject);
                pulsantiAttivi.RemoveAt(i);
                pulsantiCliccati++;
                Debug.Log($"Pulsante cliccato ({pulsantiCliccati}/{numeroPulsanti})");
                break;
            }
        }
    }

    public override bool AreConditionsSatisfied() {
        return pulsantiCliccati >= numeroPulsanti;
    }

    public override void ResetToInitialState() {
        foreach (RectTransform p in pulsantiAttivi)
            if (p != null) Destroy(p.gameObject);
        pulsantiAttivi.Clear();
        pulsantiCliccati = 0;

        SpawnTutti();
    }
}
