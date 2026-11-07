using UnityEngine;
using UnityEngine.Events;

public class Captcha : MonoBehaviour
{
    public UnityEvent<Captcha> OnComplete;
    public UnityEvent<Captcha> OnFailed;

    public Bounds bounds;

    public virtual void SetRenderLayer(int layer)
    {
        Renderer rend;
        
    }
}
