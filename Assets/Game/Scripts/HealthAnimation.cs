using Game.Scripts;
using UnityEngine;

public class HealthAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip animationClip;

    public void OnAnimationFinished()
    {
        StopAnimation();
    }
    
    public void PlayAnimation()
    {
        animator.enabled = true;
        animator.Play(animationClip.name);
    }

    public void StopAnimation()
    {
        animator.enabled = false;
        EventManager.Instance.TriggerPlayerDamageNotificationEnd(Player.Instance.GetHealth());
    }
}
