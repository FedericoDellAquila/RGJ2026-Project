using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CaptchaSeguiCerchio : Captcha {
    [SerializeField] private RectTransform areaMovimento; // zona in cui si muove il cerchio
    [SerializeField] private RectTransform cerchio;
    [SerializeField] private float velocita = 1.3f;       // unità al secondo (stessa scala dell'area)
    [SerializeField] private float scalaFinale = 0.4f;    // 0.3 = alla fine è grande il 30%
    [SerializeField] private float tempoGrazia = 1f;      // secondi prima che parta il controllo

    private Canvas canvas;
    private Image immagineCerchio;
    private Vector3 posizioneIniziale;
    private Vector3 scalaIniziale;
    private Vector2 destinazione;
    private float tempoTrascorso;
    private bool uscito;

    private void Awake() {
        canvas = cerchio.GetComponentInParent<Canvas>();
        immagineCerchio = cerchio.GetComponent<Image>();
        posizioneIniziale = cerchio.localPosition;
        scalaIniziale = cerchio.localScale;
    }

    private void OnEnable() {
        Inizializza();
    }

    private void Inizializza() {
        cerchio.localPosition = posizioneIniziale;
        cerchio.localScale = scalaIniziale;
        tempoTrascorso = 0f;
        uscito = false;
        if (immagineCerchio != null) immagineCerchio.color = Color.white;
        NuovaDestinazione();
    }

    private void Update() {
        tempoTrascorso += Time.deltaTime;

        // Restringimento graduale nell'arco del timeLimit
        float t = Mathf.Clamp01(tempoTrascorso / timeLimit);
        cerchio.localScale = Vector3.Lerp(scalaIniziale, scalaIniziale * scalaFinale, t);

        // Movimento verso la destinazione; arrivato, ne sceglie un'altra
        Vector2 pos = cerchio.localPosition;
        pos = Vector2.MoveTowards(pos, destinazione, velocita * Time.deltaTime);
        cerchio.localPosition = pos;
        if (Vector2.Distance(pos, destinazione) < 0.01f)
            NuovaDestinazione();

        // Controllo del mouse
        if (!uscito && tempoTrascorso > tempoGrazia && Mouse.current != null) {
            if (!MouseDentroCerchio(Mouse.current.position.ReadValue())) {
                uscito = true;
                Debug.Log("Mouse uscito dal cerchio!");
                if (immagineCerchio != null) immagineCerchio.color = Color.red;
            }
        }
    }

    private void NuovaDestinazione() {
        Rect area = areaMovimento.rect;
        // raggio alla dimensione attuale, così il cerchio non esce dall'area
        float rx = cerchio.rect.width * cerchio.localScale.x / 2f;
        float ry = cerchio.rect.height * cerchio.localScale.y / 2f;
        destinazione = new Vector2(
            Random.Range(area.xMin + rx, area.xMax - rx),
            Random.Range(area.yMin + ry, area.yMax - ry));
    }

    private bool MouseDentroCerchio(Vector2 posSchermo) {
        Camera cam = null;
        if (canvas.renderMode != RenderMode.ScreenSpaceOverlay)
            cam = canvas.worldCamera != null ? canvas.worldCamera : Camera.main;

        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(cerchio, posSchermo, cam, out Vector2 locale))
            return false;

        // In coordinate locali la scala è già considerata: basta il rect "originale"
        Vector2 d = locale - cerchio.rect.center;
        float rx = cerchio.rect.width / 2f;
        float ry = cerchio.rect.height / 2f;
        return (d.x * d.x) / (rx * rx) + (d.y * d.y) / (ry * ry) <= 1f;
    }

    public override bool AreConditionsSatisfied() {
        return !uscito;
    }

    public override void ResetToInitialState() {
        Inizializza();
    }
}
