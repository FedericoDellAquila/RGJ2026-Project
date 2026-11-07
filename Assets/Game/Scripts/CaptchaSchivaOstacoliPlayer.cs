using System;
using UnityEngine;

public class CaptchaSchivaOstacoliPlayer : MonoBehaviour {
    
    public event EventHandler<Collision2D> OnCollisionEntered;
   

    private void OnCollisionEnter2D(Collision2D collision) {
        OnCollisionEntered?.Invoke(this, collision);
    }

    }


