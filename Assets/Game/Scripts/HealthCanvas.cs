using Game.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthCanvas : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private Vector2 finalPosition;
    [SerializeField] private HealthAnimation healthAnimation;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject HealthUI;

    private void Awake()
    {
        EventManager.Instance.OnPlayerDamageNotificationStart.AddListener(OnPlayerDamageNotificationStartCallback);
    }

    public void Show()
    {
        healthText.text = "Health: " + Player.Instance.GetHealth() + " / " + Player.Instance.startingHealth;
        slider.value = (float)Player.Instance.GetHealth() / (float)Player.Instance.startingHealth;
        healthAnimation.PlayAnimation();
    }

    private void OnPlayerDamageNotificationStartCallback(int value)
    {
        Show();
    }
}
