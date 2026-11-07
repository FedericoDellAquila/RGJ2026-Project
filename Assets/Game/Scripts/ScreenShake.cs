using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    [SerializeField] private float duration = 0.3f;
    [SerializeField] private float magnitude = 0.2f;

    private Vector3 originalPos;
    private Coroutine shakeRoutine;

    private void Awake()
    {
        originalPos = transform.localPosition;
    }

    // Call this from anywhere: GetComponent<ScreenShake>().Shake();
    public void Shake()
    {
        Shake(duration, magnitude);
    }

    public void Shake(float shakeDuration, float shakeMagnitude)
    {
        if (shakeRoutine != null) StopCoroutine(shakeRoutine);
        shakeRoutine = StartCoroutine(DoShake(shakeDuration, shakeMagnitude));
    }

    private IEnumerator DoShake(float shakeDuration, float shakeMagnitude)
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            // Fade the shake out over time
            float strength = shakeMagnitude * (1f - elapsed / shakeDuration);
            Vector2 offset = Random.insideUnitCircle * strength;
            transform.localPosition = originalPos + new Vector3(offset.x, offset.y, 0f);

            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
        shakeRoutine = null;
    }
}
